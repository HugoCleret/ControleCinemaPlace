using ClassesMetier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        MySqlConnection cnx;
        MySqlCommand cmd;
        MySqlDataReader dr;

        public GstBdd()
        {
            string driver = "server=localhost;user id=root;password=;database=gestionsalles";
            cnx = new MySqlConnection(driver);
            cnx.Open();
        }

        public List<Manifestation> GetAllManifestations()
        {
            List<Manifestation> LesManifestations = new List<Manifestation>();
            cmd = new MySqlCommand("select idManif,nomManif,dateDebut,dateFin,idSalle,nomSalle,nbPlaces from manifestation inner join salle  where numSalle=idSalle", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Salle uneSalle = new Salle()
                {
                  IdSalle = Convert.ToInt16(dr[4].ToString()),
                  NomSalle = dr[5].ToString(),
                  NbPlaces = Convert.ToInt16(dr[6].ToString())
                };
                Manifestation uneManifestation = new Manifestation()
                {
                  IdManif = Convert.ToInt16(dr[0].ToString()),
                  NomManif = dr[1].ToString(),
                  DateDebutManif = dr[2].ToString(),
                  DateFinManif = dr[3].ToString(),
                  LaSalle = uneSalle
                };
                LesManifestations.Add(uneManifestation);
            }
            dr.Close();
            return LesManifestations;
        }

        public List<Place> GetAllPlacesByIdManifestation(int idManif, int idSalle)
        {
            List<Place> LesPlaces = new List<Place>();
            cmd = new MySqlCommand("select idPlace, idTarif, prix, libre from manifestation inner join occuper o on idManif = numManif inner join place p on numPlace = idPlace inner join tarif on idTarif = numTarif where idManif = " + idManif + " and numSalle = " + idSalle, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Place unePlace = new Place()
                {
                    IdPlace = Convert.ToInt16(dr[0].ToString()),
                    CodeTarif = Convert.ToChar(dr[1].ToString()),
                    Prix = Convert.ToDouble(dr[2].ToString()),
                    Occupee =Convert.ToBoolean(dr[3].ToString())
                    
                };
                LesPlaces.Add(unePlace);

            }
            
            dr.Close();
            return LesPlaces;
        }
     

        public List<Tarif> GetAllTarifs()
        {
            List<Tarif> lesTarifs = new List<Tarif>();
            cmd = new MySqlCommand("Select idTarif,nomTarif,prix from tarif", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tarif unTarif = new Tarif()
                {
                    IdTarif = Convert.ToChar(dr[0].ToString()),
                    NomTarif = dr[1].ToString(),
                    Prix = Convert.ToDouble(dr[2].ToString())
                    
                };
                lesTarifs.Add(unTarif);
            }
            dr.Close();
            return lesTarifs;

        }

        public void ReserverPlace(int idPlace, int idSalle,int idManif)
        {
            //char etat;

            //while (dr.Read())
            //{
            //    if(dr[])
            //}
        }
    }
}
