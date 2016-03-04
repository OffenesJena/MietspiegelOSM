/*
 * Copyright (c) 2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of OpenDataAPI <http://www.github.com/GraphDefined/OpenDataAPI>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using org.GraphDefined.OpenDataAPI.OverpassAPI;
using org.GraphDefined.Vanaheimr.BouncyCastle;

#endregion

namespace MietspiegelOSM
{

    public class Program
    {

        public static void Main(String[] Arguments)
        {

            var SecretKey    = OpenPGP.ReadSecretKey(File.OpenRead("jod-official-secring.gpg"));
            var Passphrase   = File.ReadAllText("jod-official-passphrase.txt");

            Directory.CreateDirectory("ÖffentlicherNahverkehr");
            Directory.CreateDirectory("Gebäude");
            Directory.CreateDirectory("Strassen");
            Directory.CreateDirectory("Tempolimits");

            var JenaId       = new OverpassQuery("Jena").AreaId;
//            var ThüringenId  = new OverpassQuery("Thüringen").AreaId;


            #region Gebäude

            new OverpassQuery(JenaId).
                WithAny      ("building").
                RunAll("Gebäude/buildings",
                       SecretKey, Passphrase);

            #endregion

            #region Strassen

            // highway (nodes/relations)

            new OverpassQuery(JenaId).
                WithNodes    ("highway").
                RunAll       ("Strassen/highway-nodes",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                RunAll       ("Strassen/highway-ways",
                              SecretKey, Passphrase);

            #endregion

            #region Tempolimits

            new OverpassQuery(JenaId).
                WithNodes    ("highway", "speed_camera").
                RunAll       ("Tempolimits/Blitzer",
                              SecretKey, Passphrase);

            // ToDo: In this case it's not that simple! Split and merge GeoJSON has to be implemented!
            //new OverpassQuery(JenaId).
            //    WithWays     ("highway").
            //    And          ("maxspeed").
            //    ToGeoJSON    ().
            //    SplitFeatures().
            //    ToGeoJSONFile(JSON => "Tempolimits/" + JSON["features"][0]["properties"]["maxspeed"].ToString() + ".geojson").
            //    RunNow();

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "5").
                RunAll       ("Tempolimits/Tempo5",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "6").
                RunAll       ("Tempolimits/Tempo6",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "10").
                RunAll       ("Tempolimits/Tempo10",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "20").
                RunAll       ("Tempolimits/Tempo20",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "30").
                RunAll       ("Tempolimits/Tempo30",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "40").
                RunAll       ("Tempolimits/Tempo40",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "50").
                RunAll       ("Tempolimits/Tempo50",
                              SecretKey, Passphrase);


            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "60").
                RunAll       ("Tempolimits/Tempo60",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "70").
                RunAll       ("Tempolimits/Tempo70",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "80").
                RunAll       ("Tempolimits/Tempo80",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "90").
                RunAll       ("Tempolimits/Tempo90",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "100").
                RunAll       ("Tempolimits/Tempo100",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "110").
                RunAll       ("Tempolimits/Tempo110",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "120").
                RunAll       ("Tempolimits/Tempo120",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithWays     ("highway").
                And          ("maxspeed", "130").
                RunAll       ("Tempolimits/Tempo130",
                              SecretKey, Passphrase);

            #endregion

            #region Öffentlicher Nahverkehr

            // http://wiki.openstreetmap.org/wiki/DE:Relation:route
            // trolleybus, share_taxi

            new OverpassQuery(JenaId).
                WithRelations("route",   "bus").
                WithNodes    ("highway", "bus_stop").
                WithNodes    ("amenity", "bus_station").
                RunAll       ("ÖffentlicherNahverkehr/Buslinien",
                              SecretKey, Passphrase);

            new OverpassQuery(JenaId).
                WithRelations("route",   "tram").
                WithNodes    ("railway", "tram_stop").
                RunAll       ("ÖffentlicherNahverkehr/Strassenbahnen",
                              SecretKey, Passphrase);

            #endregion

        }

    }

}
