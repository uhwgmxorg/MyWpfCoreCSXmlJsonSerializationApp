//using System.Text.Json;

using System.Text.Json;

namespace MyWpfCoreCSXmlJsonSerializationApp
{
    public class LST
    {
        /******************************/
        /*   Load and Save Functions  */
        /******************************/
        #region Load and Save Functions
        #region Xml

        /// <summary>
        /// SaveClassToXml
        /// Save a class T to a Xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="file"></param>
        static public void SaveClassToXml<T>(T obj, string file)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (System.IO.StreamWriter wr = new System.IO.StreamWriter(file))
                {
                    xs.Serialize(wr, obj);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// LoadClassFromXml
        /// Load a class T from a Xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        static public bool LoadClassFromXml<T>(ref T obj, string file)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                using (System.IO.StreamReader rd = new System.IO.StreamReader(file))
                {
                    var Obj = serializer.Deserialize(rd);
                    obj = (T)Obj;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// SaveListToXml
        /// Save a list to a Xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        static public void SaveListToXml<T>(System.Collections.Generic.List<T> list, string file)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<T>));
                //XmlSerializer xs = XmlSerializer.FromTypes(new[] { typeof(List<T>) })[0];
                using (System.IO.StreamWriter wr = new System.IO.StreamWriter(file))
                {
                    xs.Serialize(wr, list);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// LoadListFromXml
        /// Load a list from a Xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public System.Collections.Generic.List<T> LoadListFromXml<T>(string file)
        {
            System.Collections.Generic.List<T> list = null;

            try
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<T>));
                //XmlSerializer xs = XmlSerializer.FromTypes(new[] { typeof(List<T>) })[0];

                using (System.IO.StreamReader rd = new System.IO.StreamReader(file))
                {
                    list = xs.Deserialize(rd) as System.Collections.Generic.List<T>;
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                list = new System.Collections.Generic.List<T>();
            }
            return list;
        }

        #endregion
        #region Json

        /// <summary>
        /// SaveClassToJson
        /// For this we need System.Text.Json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="file"></param>
        static public void SaveClassToJson<T>(T obj, string file)
        {
            try
            {
                byte[] jsonUtf8Bytes;
                var options = new JsonSerializerOptions { WriteIndented = false }; // <-- true for better human readable

                jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(obj, options);

                string byteString = System.Text.Encoding.UTF8.GetString(jsonUtf8Bytes);
                using (System.IO.StreamWriter wr = new System.IO.StreamWriter(file))
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(byteString);
                    wr.Write(sb);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// LoadClassFromJson
        /// For this we need System.Text.Json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        static public bool LoadClassFromJson<T>(ref T obj, string file)
        {
            try
            {
                string byteString = "";
                int BufferSize = 128;
                using (var rd = new System.IO.StreamReader(file, System.Text.Encoding.UTF8, true, BufferSize))
                {
                    string line;
                    while ((line = rd.ReadLine()) != null)
                        byteString += line;
                }

                obj = JsonSerializer.Deserialize<T>(byteString);
                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// SaveListToJson
        /// Save a list to a Json file
        /// For this we need System.Text.Json 
        /// NuGet:
        /// Install-Package System.Text.Json -Version 5.0.1
        /// Update-Package System.Text.Json  -ignoreDependencies
        /// Uninstall-Package System.Text.Json
        /// For nuget commandes see:
        /// https://www.syncfusion.com/blogs/post/how-to-use-nuget-packages.aspx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="file"></param>
        static public void SaveListToJson<T>(System.Collections.Generic.List<T> list, string file)
        {
            try
            {
                byte[] jsonUtf8Bytes;
                var options = new JsonSerializerOptions { WriteIndented = false }; // <-- true for better human readable

                jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(list, options);

                string byteString = System.Text.Encoding.UTF8.GetString(jsonUtf8Bytes);
                using (System.IO.StreamWriter wr = new System.IO.StreamWriter(file))
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(byteString);
                    wr.Write(sb);
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// LoadListFromJson
        /// For this we need System.Text.Json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="file"></param>
        static public System.Collections.Generic.List<T> LoadListFromJson<T>(string file)
        {
            try
            {
                string byteString = "";
                int BufferSize = 128;
                using (var rd = new System.IO.StreamReader(file, System.Text.Encoding.UTF8, true, BufferSize))
                {
                    string line;
                    while ((line = rd.ReadLine()) != null)
                        byteString += line;
                }

                var list = JsonSerializer.Deserialize<System.Collections.Generic.List<T>>(byteString);
                return list;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        #endregion
        #endregion

        /******************************/
        /*      Convert Functions     */
        /******************************/
        #region Convert Functions

        /// <summary>
        /// OToL
        /// Convert a ObservableCollection<T> to a List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="olist"></param>
        /// <returns></returns>
        static public System.Collections.Generic.List<T> OToL<T>(System.Collections.ObjectModel.ObservableCollection<T> olist)
        {
            System.Collections.Generic.List<T> l = new System.Collections.Generic.List<T>(olist);
            return l;
        }

        /// <summary>
        /// LToO
        /// converting a List<T> to ObservableCollection<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="llist"></param>
        /// <returns></returns>
        static public System.Collections.ObjectModel.ObservableCollection<T> LToO<T>(System.Collections.Generic.List<T> llist)
        {
            var oc = new System.Collections.ObjectModel.ObservableCollection<T>();
            foreach (var item in llist)
                oc.Add(item);
            return oc;
        }

        #endregion

        /******************************/
        /*       Other Functions      */
        /******************************/
        #region Other Functions

        /// <summary>
        /// CreateDirektory
        /// </summary>
        /// <param name="dir"></param>
        static public void CreateDirektory(string dir)
        {
            try
            {
                bool isExists = System.IO.Directory.Exists(dir);
                if (!isExists)
                    System.IO.Directory.CreateDirectory(dir);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// DeleteAllFiles
        /// </summary>
        /// <param name="dir"></param>
        static public void DeleteAllFiles(string dir)
        {
            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);

                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Generic Min
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T Min<T>(T x, T y)
        {
            return (System.Collections.Generic.Comparer<T>.Default.Compare(x, y) < 0) ? x : y;
        }

        /// <summary>
        /// GetFilesFomOpenFileDialog
        /// Opens an OpenFile standard dialog and returns an array of selected files
        /// ATTENTION:
        /// To be able to compile this function, the variable 'DefaultPath' is necessary in the settings!
        /// </summary>
        /// <returns></returns>
        public static string[] GetFilesFromOpenFileDialog()
        {
            string[] files = null;
            string path = Properties.Settings.Default.DefaultPath;

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.InitialDirectory = path;
            ofd.Multiselect = true;
            ofd.DefaultExt = ".xml"; // Default file extension
            ofd.Filter = "xml (.xml)|*.xml"; // Filter files by extension

            ofd.ShowDialog();
            files = ofd.FileNames;
            try { Properties.Settings.Default.DefaultPath = System.IO.Path.GetDirectoryName(files[0]); }
            catch (System.Exception) { }

            return files;
        }

        #endregion

        /***********************************************/
        /* Specific functions for the specific program */
        /***********************************************/
        #region Specific functions for the specific program

        /// <summary>
        /// PopulateData
        /// </summary>
        /// <returns></returns>
        static public System.Collections.ObjectModel.ObservableCollection<Data> PopulateData()
        {
            int count = 1;
            System.Collections.ObjectModel.ObservableCollection<Data> initialList = new System.Collections.ObjectModel.ObservableCollection<Data>();

            #region Student data
            // from http://www.generatedata.com
            var data = new[] {
                new { FirstName = "James", SurName = "Mitchell" },
                new { FirstName = "Jelani", SurName = "Cruz" },
                new { FirstName = "Driscoll", SurName = "Compton" },
                new { FirstName = "Mufutau", SurName = "Haynes" },
                new { FirstName = "Chaim", SurName = "Farmer" },
                new { FirstName = "Charles", SurName = "Holman" },
                new { FirstName = "Armand", SurName = "Cote" },
                new { FirstName = "Grant", SurName = "Maldonado" },
                new { FirstName = "Devin", SurName = "Oneil" },
                new { FirstName = "Demetrius", SurName = "Case" },
                new { FirstName = "Brendan", SurName = "York" },
                new { FirstName = "Porter", SurName = "Gibson" },
                new { FirstName = "Abbot", SurName = "Crane" },
                new { FirstName = "Peter", SurName = "Hendricks" },
                new { FirstName = "Cruz", SurName = "Sloan" },
                new { FirstName = "Alec", SurName = "Velasquez" },
                new { FirstName = "Leroy", SurName = "Drake" },
                new { FirstName = "Ryan", SurName = "Grimes" },
                new { FirstName = "Aquila", SurName = "Gomez" },
                new { FirstName = "Octavius", SurName = "Chang" },
                new { FirstName = "Ezra", SurName = "Blevins" },
                new { FirstName = "Rajah", SurName = "Whitaker" },
                new { FirstName = "Timothy", SurName = "Booker" },
                new { FirstName = "Ishmael", SurName = "Hill" },
                new { FirstName = "Ferris", SurName = "Arnold" },
                new { FirstName = "Keane", SurName = "Bishop" },
                new { FirstName = "Jackson", SurName = "Park" },
                new { FirstName = "Xenos", SurName = "Nolan" },
                new { FirstName = "Hilel", SurName = "Tucker" },
                new { FirstName = "Kenneth", SurName = "Matthews" },
                new { FirstName = "Benjamin", SurName = "Norman" },
                new { FirstName = "Martin", SurName = "Albert" },
                new { FirstName = "Jakeem", SurName = "Ellis" },
                new { FirstName = "Colin", SurName = "Craft" },
                new { FirstName = "Guy", SurName = "Henson" },
                new { FirstName = "Fritz", SurName = "Hernandez" },
                new { FirstName = "Hector", SurName = "Cain" },
                new { FirstName = "Aaron", SurName = "Langley" },
                new { FirstName = "Ross", SurName = "Marshall" },
                new { FirstName = "Hoyt", SurName = "Foley" },
                new { FirstName = "Cairo", SurName = "Woodard" },
                new { FirstName = "Macaulay", SurName = "Hinton" },
                new { FirstName = "Allistair", SurName = "Terrell" },
                new { FirstName = "Samson", SurName = "Hubbard" },
                new { FirstName = "Knox", SurName = "Ayala" },
                new { FirstName = "Ahmed", SurName = "Hood" },
                new { FirstName = "Emerson", SurName = "Thornton" },
                new { FirstName = "Noah", SurName = "Allen" },
                new { FirstName = "Marsden", SurName = "Dodson" },
                new { FirstName = "Nash", SurName = "Pennington" },
                new { FirstName = "Randall", SurName = "Beach" },
                new { FirstName = "Kadeem", SurName = "Hurley" },
                new { FirstName = "Odysseus", SurName = "Anderson" },
                new { FirstName = "Xander", SurName = "Mcintyre" },
                new { FirstName = "Davis", SurName = "Moss" },
                new { FirstName = "Hall", SurName = "Valentine" },
                new { FirstName = "Ethan", SurName = "Oneal" },
                new { FirstName = "Macon", SurName = "Farrell" },
                new { FirstName = "Quamar", SurName = "Aguilar" },
                new { FirstName = "Lance", SurName = "Shelton" },
                new { FirstName = "Connor", SurName = "Puckett" },
                new { FirstName = "Herman", SurName = "Sexton" },
                new { FirstName = "Vance", SurName = "Gallegos" },
                new { FirstName = "Byron", SurName = "Collier" },
                new { FirstName = "Dorian", SurName = "Aguilar" },
                new { FirstName = "Lawrence", SurName = "Bush" },
                new { FirstName = "Knox", SurName = "Kent" },
                new { FirstName = "Randall", SurName = "Simmons" },
                new { FirstName = "Howard", SurName = "Ryan" },
                new { FirstName = "Otto", SurName = "Hardin" },
                new { FirstName = "Jordan", SurName = "Velasquez" },
                new { FirstName = "Elvis", SurName = "Duran" },
                new { FirstName = "Merrill", SurName = "Saunders" },
                new { FirstName = "Dieter", SurName = "Ross" },
                new { FirstName = "Kelly", SurName = "Frank" },
                new { FirstName = "Aristotle", SurName = "Mathews" },
                new { FirstName = "Arthur", SurName = "Burt" },
                new { FirstName = "Slade", SurName = "Powers" },
                new { FirstName = "Emmanuel", SurName = "Elliott" },
                new { FirstName = "Warren", SurName = "Andrews" },
                new { FirstName = "Fulton", SurName = "Schneider" },
                new { FirstName = "Amir", SurName = "Oliver" },
                new { FirstName = "Hyatt", SurName = "Butler" },
                new { FirstName = "Barrett", SurName = "Harris" },
                new { FirstName = "Ava", SurName = "Blackburn" },
                new { FirstName = "Shaeleigh", SurName = "Henson" },
                new { FirstName = "Camilla", SurName = "Ewing" },
                new { FirstName = "Maya", SurName = "Conner" },
                new { FirstName = "Brenna", SurName = "Perkins" },
                new { FirstName = "Tanisha", SurName = "Mercado" },
                new { FirstName = "Regina", SurName = "Kaufman" },
                new { FirstName = "Kyla", SurName = "Haynes" },
                new { FirstName = "Dorothy", SurName = "Chase" },
                new { FirstName = "Sloane", SurName = "Donaldson" },
                new { FirstName = "Whitney", SurName = "Nielsen" },
                new { FirstName = "Flavia", SurName = "Kane" },
                new { FirstName = "Nayda", SurName = "Burks" },
                new { FirstName = "Tasha", SurName = "Cortez" },
                new { FirstName = "Rylee", SurName = "Mercado" },
                new { FirstName = "Cherokee", SurName = "Noble" },
                new { FirstName = "Emerald", SurName = "Marquez" },
                new { FirstName = "Nichole", SurName = "Joyce" },
                new { FirstName = "Hope", SurName = "Guthrie" },
                new { FirstName = "Hedda", SurName = "Davis" },
                new { FirstName = "Quynn", SurName = "Middleton" },
                new { FirstName = "Mechelle", SurName = "Allen" },
                new { FirstName = "Jael", SurName = "Irwin" },
                new { FirstName = "Giselle", SurName = "Wheeler" },
                new { FirstName = "Rhona", SurName = "Odom" },
                new { FirstName = "Pascale", SurName = "Ramsey" },
                new { FirstName = "Eliana", SurName = "Lowe" },
                new { FirstName = "Karly", SurName = "Hurst" },
                new { FirstName = "Petra", SurName = "Kramer" },
                new { FirstName = "Macey", SurName = "Hayden" },
                new { FirstName = "Demetria", SurName = "Witt" },
                new { FirstName = "Jeanette", SurName = "Doyle" },
                new { FirstName = "Nevada", SurName = "Graham" },
                new { FirstName = "Neve", SurName = "Mcknight" },
                new { FirstName = "Renee", SurName = "Stout" },
                new { FirstName = "Winifred", SurName = "Walls" },
                new { FirstName = "Kevyn", SurName = "Marsh" },
                new { FirstName = "Miriam", SurName = "Vaughn" },
                new { FirstName = "Miranda", SurName = "Strickland" },
                new { FirstName = "Zenia", SurName = "Hanson" },
                new { FirstName = "Maxine", SurName = "Chambers" },
                new { FirstName = "Nelle", SurName = "Patterson" },
                new { FirstName = "Gay", SurName = "Schmidt" },
                new { FirstName = "Octavia", SurName = "Eaton" },
                new { FirstName = "Sydnee", SurName = "Harmon" },
                new { FirstName = "Robin", SurName = "Ayala" },
                new { FirstName = "Kylynn", SurName = "Sullivan" },
                new { FirstName = "Yoko", SurName = "Hatfield" },
                new { FirstName = "Darrel", SurName = "Farley" },
                new { FirstName = "Lenore", SurName = "Merritt" },
                new { FirstName = "Gail", SurName = "Ford" },
                new { FirstName = "Alfreda", SurName = "Green" },
                new { FirstName = "Jenna", SurName = "Lester" },
                new { FirstName = "Germane", SurName = "Cherry" },
                new { FirstName = "Carly", SurName = "England" },
                new { FirstName = "Doris", SurName = "Burt" },
                new { FirstName = "Cora", SurName = "Navarro" },
                new { FirstName = "Lacota", SurName = "Hodges" },
                new { FirstName = "Zoe", SurName = "Gilbert" },
                new { FirstName = "Aimee", SurName = "Wolfe" },
                new { FirstName = "Martena", SurName = "Gamble" },
                new { FirstName = "Rhea", SurName = "Humphrey" },
                new { FirstName = "TaShya", SurName = "Nelson" },
                new { FirstName = "Heidi", SurName = "Kirk" },
                new { FirstName = "India", SurName = "Stuart" },
                new { FirstName = "Rinah", SurName = "Hanson" },
                new { FirstName = "Sade", SurName = "Mccarty" },
                new { FirstName = "Azalia", SurName = "Reid" },
                new { FirstName = "Isadora", SurName = "Hicks" },
                new { FirstName = "Quemby", SurName = "Ayers" },
                new { FirstName = "Shellie", SurName = "Hebert" },
                new { FirstName = "Britanney", SurName = "Harper" },
                new { FirstName = "Amanda", SurName = "Carney" },
                new { FirstName = "Montana", SurName = "Briggs" },
                new { FirstName = "Cheryl", SurName = "Bird" },
                new { FirstName = "Odette", SurName = "Cline" },
                new { FirstName = "Tatiana", SurName = "Maldonado" },
                new { FirstName = "Shea", SurName = "Wall" },
                new { FirstName = "Jennifer", SurName = "Thornton" },
                new { FirstName = "Charde", SurName = "Lloyd" },
                new { FirstName = "Zenaida", SurName = "Woodard" },
                new { FirstName = "Whilemina", SurName = "Brennan" },
                new { FirstName = "Kylan", SurName = "Weeks" },
                new { FirstName = "Felicia", SurName = "Rojas" },
                new { FirstName = "Lacota", SurName = "Chambers" },
                new { FirstName = "Amanda", SurName = "George" },
                new { FirstName = "Ruby", SurName = "Hickman" },
                new { FirstName = "Desirae", SurName = "Knowles" },
                new { FirstName = "Autumn", SurName = "Little" },
                new { FirstName = "Meghan", SurName = "Fulton" },
                new { FirstName = "Aubrey", SurName = "Stevens" },
                new { FirstName = "Zenaida", SurName = "Roberson" },
                new { FirstName = "Camille", SurName = "Warner" },
                new { FirstName = "Tana", SurName = "Ray" },
                new { FirstName = "Xena", SurName = "Lindsey" },
                new { FirstName = "Teegan", SurName = "Pacheco" },
                new { FirstName = "Lara", SurName = "Cohen" },
                new { FirstName = "Cassidy", SurName = "Knowles" },
                new { FirstName = "Wanda", SurName = "Tate" },
                new { FirstName = "Wendy", SurName = "Snyder" },
                new { FirstName = "Reed", SurName = "Soto" },
                new { FirstName = "Malcolm", SurName = "Duke" },
                new { FirstName = "Damian", SurName = "Melendez" },
                new { FirstName = "Quamar", SurName = "Jennings" },
                new { FirstName = "Bevis", SurName = "Stout" },
                new { FirstName = "Gil", SurName = "Wilkinson" },
                new { FirstName = "Simon", SurName = "Chen" },
                new { FirstName = "Silas", SurName = "Terrell" },
                new { FirstName = "Lewis", SurName = "Welch" },
                new { FirstName = "Lionel", SurName = "Flynn" },
                new { FirstName = "Stephen", SurName = "Fuentes" },
                new { FirstName = "Ezekiel", SurName = "Maddox" },
                new { FirstName = "Brett", SurName = "Rosales" },
                new { FirstName = "Amal", SurName = "Hale" },
                new { FirstName = "Arsenio", SurName = "Farrell" },
                new { FirstName = "James", SurName = "Velazquez" }
            };

            foreach (var n in data)
                initialList.Add(new Data { Id = count++, Name = n.FirstName });
            #endregion

            return initialList;
        }

        #endregion
    }
}
