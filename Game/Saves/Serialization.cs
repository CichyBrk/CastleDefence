using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.XPath;

namespace Game
{
    class Serialization
    {
        /// <summary>
        /// Zapisywanie listy wyników graczy do pliku
        /// </summary>
        /// <param name="playerList"></param>
        static public void serialize(List<PlayerScore> playerList)
            {
                XmlRootAttribute oRootAttr = new XmlRootAttribute();
                oRootAttr.ElementName = "Saves";
                oRootAttr.IsNullable = true;
                XmlSerializer oSerializer = new XmlSerializer(typeof(List<PlayerScore>), oRootAttr);
                StreamWriter oStreamWriter = null;
                try
                {
                    oStreamWriter = new StreamWriter("DataBase.xml");
                    oSerializer.Serialize(oStreamWriter, playerList);
                }
                catch (Exception oException)
                {
                    Console.WriteLine("Aplikacja wygenerowała następujący wyjątek: " + oException.Message);
                }
                finally
                {
                    if (null != oStreamWriter)
                    {
                        oStreamWriter.Dispose();
                    }
                }
            }

        /// <summary>
        /// Odczyt listy wyników graczy z pliku
        /// </summary>
        /// <returns> lista wyników graczy </returns>
        static public List<PlayerScore> Deserializacja()
            {
                XPathDocument oXPathDocument = new XPathDocument("DataBase.xml");
                XPathNavigator oXPathNavigator = oXPathDocument.CreateNavigator();
                XPathNodeIterator oPersonNodesIterator = oXPathNavigator.Select("/Saves/PlayerScore");

                List<PlayerScore> playerList = new List<PlayerScore>();
                foreach (XPathNavigator oCurrentPerson in oPersonNodesIterator)
                {
                    PlayerScore player = new PlayerScore(oCurrentPerson.SelectSingleNode("name").Value,
                    Convert.ToInt32(oCurrentPerson.SelectSingleNode("experience").Value));

                    playerList.Add(player);
                }

                return playerList;
            }
    }
}
