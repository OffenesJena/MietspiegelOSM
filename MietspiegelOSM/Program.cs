using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using org.GraphDefined.OpenDataAPI.OverpassAPI;


namespace MietspiegelOSM
{

    public class Program
    {

        public static void Main(String[] Arguments)
        {

            var JenaId      = new OverpassQuery("Jena").AreaId;
            var ThüringenId = new OverpassQuery("Thüringen").AreaId;


            #region Gebäude

            var a = new OverpassQuery(JenaId).
                        WithAny      ("building").
                        ToGeoJSONFile("JenaBuildings.geojson").
                        Result;

            #endregion

        }

    }

}
