/// <author>Lukas Eibensteiner</author>
/// <date>19.02.2013</date>
/// <summary>Example of a Wavefront OBJ 3D model importer</summary>

using System.Numerics;
//using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Games
{
    /// <summary>
    ///     Class for reading a 3D mesh in the Wavefront OBJ format from a stream.
    /// </summary>
    public class WavefrontReader
    {
        /// <summary>
        /// Enum for describing the semantic meaning of a line in an OBJ file.
        /// </summary>
        private enum DataType
        {
            /// <summary>
            /// The line contains nothing or has no or an undefined keyword.
            /// </summary>
            Empty,

            /// <summary>
            /// The line contains a comment.
            /// </summary>
            Comment,

            /// <summary>
            /// The line contains a group definition.
            /// </summary>
            Group,

            /// <summary>
            /// The line contains a smoothing group definitio.
            /// </summary>
            SmoothingGroup,

            /// <summary>
            /// The line contains a position vector definition.
            /// </summary>
            Position,

            /// <summary>
            /// The line contains a normal vector definition.
            /// </summary>
            Normal,

            /// <summary>
            /// The line contains a texture coordinate definition.
            /// </summary>
            TexCoord,

            /// <summary>
            /// The line contains a face definition.
            /// </summary>
            Face,
        }

        // Dictionary mapping the DataType enumeration to the corresponding keyword.
        private static Dictionary<DataType, string> Keywords
        {
            get
            {
                return new Dictionary<DataType, string>()
                {
                    { DataType.Comment,         "#"     },
                    { DataType.Group,           "g"     },
                    { DataType.SmoothingGroup,  "s"     },
                    { DataType.Position,        "v"     },
                    { DataType.TexCoord,        "vt"    },
                    { DataType.Normal,          "vn"    },
                    { DataType.Face,            "f"     },
                };
            }
        }

        /// <summary>
        ///     Reads a WavefrontObject instance from the stream.
        /// </summary>
        /// <param name="stream">
        ///     Stream containing the OBJ file content.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stream"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IOException">
        ///     Error while reading from the stream.
        /// </exception>
        public WavefrontObject Read(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            // Create the stream reader for the file
            var reader = new StreamReader(stream);

            // Store the lines here
            var lines = new List<string>();

            // Store the current line here
            var current = string.Empty;

            // Read the file line by line and normalize them
            while ((current = reader.ReadLine()) != null)
                lines.Add(NormalizeLine(current));

            // Create empty mesh instance
            var obj = new WavefrontObject();

            // Iterate over all lines
            foreach (string line in lines)
            {
                // Get line type and content
                DataType type = GetType(line);
                string content = GetContent(line, type);

                // Line is a position
                if (type == DataType.Position)
                    obj.Positions.Add(ParseVector3(content));

                // Line is a texture coordinate
                if (type == DataType.TexCoord)
                    obj.Texcoords.Add(ParseVector2(content));

                // Line is a normal vector
                if (type == DataType.Normal)
                    obj.Normals.Add(ParseVector3(content));

                // Line is a mesh sub group
                if (type == DataType.Group)
                    obj.Groups.Add(new WavefrontFaceGroup() { Name = content });

                // Line is a polygon
                if (type == DataType.Face)
                {
                    // Create the default group for all faces outside a group
                    if (obj.Groups.Count == 0)
                        obj.Groups.Add(new WavefrontFaceGroup());

                    // Add the face to the last group added
                    obj.Groups.Last().Faces.Add(ParseFace(content));
                }
            }

            return obj;
        }

        // Trim beginning and end and collapse all whitespace in a string to single space.
        private string NormalizeLine(string line)
        {
            return System.Text.RegularExpressions.Regex.Replace(line.Trim(), @"\s+", " ");
        }

        // Get the type of data stored in the specified line.
        private DataType GetType(string line)
        {
            // Iterate over the keywords
            foreach (var item in Keywords)
            {
                var type = item.Key;
                var keyword = item.Value;

                // Line starts with current keyword
                if (line.ToLower().StartsWith(keyword.ToLower() + " "))
                {
                    // Return current type
                    return type;
                }
            }

            // No type
            return DataType.Empty;
        }

        // Remove the keyword from the start of the line and return the result.
        // Returns an empty string if the specified type was DataType.Empty.
        private string GetContent(string line, DataType type)
        {
            // If empty return empty string,
            // else remove the keyword from the start
            return type == DataType.Empty
                ? string.Empty
                : line.Substring(Keywords[type].Length).Trim();
        }

        // Create an array of floats of arbitary length from a string representation,
        // where the floats are spearated by whitespace.
        private static float[] ParseFloatArray(string str, int count)
        {
            var floats = new float[count];

            var segments = str.Split(' ');

            for (int i = 0; i < count; i++)
            {
                if (i < segments.Length)
                {
                    try
                    {
                        floats[i] = float.Parse(segments[i], System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        floats[i] = 0;
                    }
                }
            }

            return floats;
        }

        // Parse a 3D vector from a string definition in the form of: 2.0 3.0 1.0
        private Vector2 ParseVector2(string str)
        {
            var components = ParseFloatArray(str, 3);

            var vec = new Vector2(components[0], components[1]);

            return components[2] == 0
                ? vec
                : vec / components[2];
        }

        // Parse a 3D vector from a string definition in the form of: 1.0 2.0 3.0 1.0
        private Vector3 ParseVector3(string str)
        {
            var components = ParseFloatArray(str, 4);

            var vec = new Vector3(components[0], components[1], components[2]);

            return components[3] == 0
                ? vec
                : vec / components[3];
        }

        // Parse a OBJ face from a string definition.
        private WavefrontFace ParseFace(string str)
        {
            // Split the face definition at whitespace
            var segments = str.Split(new Char[0], StringSplitOptions.RemoveEmptyEntries);

            var vertices = new List<WavefrontVertex>();

            // Iterate over the segments
            foreach (string segment in segments)
            {
                // Parse and add the vertex
                vertices.Add(ParseVertex(segment));
            }

            // Create and return the face
            return new WavefrontFace()
            {
                Vertices = vertices,
            };
        }

        // Parse an OBJ vertex from a string definition in the forms of: 
        //     1/2/3
        //     1//3
        //     1/2
        //     1
        private WavefrontVertex ParseVertex(string str)
        {
            // Split the string definition at the slash separator
            var segments = str.Split('/');

            // Store the vertex indices here
            var indices = new int[3];

            // Iterate 3 times
            for (int i = 0; i < 3; i++)
            {
                // If no segment exists at the location or the segment can not be passed to an integer
                // Set the index to zero
                if (segments.Length <= i || !int.TryParse(segments[i], out indices[i]))
                    indices[i] = 0;
            }

            // Create the new vertex
            return new WavefrontVertex()
            {
                Position = indices[0],
                Texcoord = indices[1],
                Normal = indices[2],
            };
        }
    }

    /// <summary>
    ///     Class representing a Wavefront OBJ 3D mesh.
    /// </summary>
    public class WavefrontObject
    {
        public WavefrontObject()
        {
            Groups = new List<WavefrontFaceGroup>();
            Positions = new List<Vector3>();
            Texcoords = new List<Vector2>();
            Normals = new List<Vector3>();
        }

        // Lists containing the vertex components
        public List<Vector3> Positions { get; private set; }
        public List<Vector2> Texcoords { get; private set; }
        public List<Vector3> Normals { get; private set; }

        // List of sub meshes
        public List<WavefrontFaceGroup> Groups { get; private set; }
    }

    /// <summary>
    ///     Struct representing an Wavefront OBJ face group.
    /// </summary>
    /// <remarks>
    ///     Groups contain faces and subdivide a geometry into smaller objects.
    /// </remarks>
    public class WavefrontFaceGroup
    {
        public WavefrontFaceGroup()
        {
            Faces = new List<WavefrontFace>();
        }

        // Name of the sub mesh
        public string Name { get; set; }

        // A list of faces
        public List<WavefrontFace> Faces { get; set; }

        // Get the total number of triangles
        public int TriangleCount
        {
            get
            {
                var count = 0;

                foreach (var face in Faces)
                    count += face.TriangleCount;

                return count;
            }
        }
    }

    /// <summary>
    ///     A struct representing a Wavefront OBJ geometry face.
    /// </summary>
    /// <remarks>
    ///     A face is described through a list of OBJ vertices.
    ///     It can consist of three or more vertices an can therefore be split up
    ///     into one or more triangles.
    /// </remarks>
    public struct WavefrontFace
    {
        public List<WavefrontVertex> Vertices { get; set; }

        // Number of triangles the face (polygon) consists of
        public int TriangleCount
        {
            get
            {
                return Vertices.Count - 2;
            }
        }

        // Number of vertices
        public int VertexCount
        {
            get { return Vertices.Count; }
        }
    }

    /// <summary>
    ///     A struct representing an Wavefront OBJ vertex. 
    /// </summary>
    /// <remarks>
    ///     OBJ vertices are indexed vertices so instead of vectors 
    ///     it has an index for the position, texture coordinate and normal.
    ///     Each of those indices points to a location in a list of vectors.
    /// </remarks>
    public struct WavefrontVertex
    {
        public WavefrontVertex(int position, int texcoord, int normal)
            : this()
        {
            Position = position;
            Texcoord = texcoord;
            Normal = normal;
        }

        // Inidices of the vertex components
        public int Position { get; set; }
        public int Normal { get; set; }
        public int Texcoord { get; set; }
    }
}