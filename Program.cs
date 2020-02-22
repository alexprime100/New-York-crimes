using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;

namespace bdd
{
    class Program
    {
		//remplacez les esilvs6 par vos identifiants
        public static string connectionString = "SERVER=localhost;PORT=3306;DATABASE=NY_Crimes;UID=esilvs6;PASSWORD=esilvs6;SSLMODE=none;";

        static void Main(string[] args)
        {
            bool fin = false;
            bool valid = true;
            string lecture = " ";

            //Menu interactif
            //---------------
            do
            {
                fin = false;
                //
                Console.WriteLine();
                Console.WriteLine("1 : Importer une journées de crimes");
                Console.WriteLine("2 : Exporter le bilan journalier");
                Console.WriteLine("3 : Saisie d'un crime");
                Console.WriteLine("4 : Le nombre de crimes par quartier et par catégorie");
                Console.WriteLine("5 : Récapitulatif pour un mois");
                Console.WriteLine("6 : Evolution mois par mois");
                Console.WriteLine("7 : Palmarès annuel");
                Console.WriteLine("8 : Nombre total de crimes selon ville et année pour chaque crime");
                Console.WriteLine("9 : fin");
                //
                do
                {
                    lecture = "";
                    valid = true;

                    Console.Write("\nchoisissez un programme > ");
                    lecture = Console.ReadLine();
                    Console.WriteLine(lecture);
                    if (lecture == "" || !"123456789".Contains(lecture[0]))
                    {
                        Console.WriteLine("votre choix <" + lecture + "> n'est pas valide = > recommencez ");
                        valid = false;
                    }
                } while (!valid);
                //
                //
                switch (lecture[0])
                {
                    case '1':
                        Console.Clear();
                        InsertionJournee();
                        break;
                    case '2':
                        Console.Clear();
                        ExporterBilanJournalier();
                        break;
                    case '3':
                        Console.Clear();
                        SaisirUnCrime();
                        break;
                    case '4':
                        Console.Clear();
                        CrimesParQuartier();
                        break;
                    case '5':
                        Console.Clear();
                        RecapitulatifMensuel();
                        break;
                    case '6':
                        Console.Clear();
                        EvolutionMoisParMois();
                        break;
                    case '7':
                        Console.Clear();
                        PalmaresAnnuel();
                        break;
                    case '8':
                        Console.Clear();
                        VotreFonction();
                        break;
                    case '9':
                        Console.Clear();
                        Console.WriteLine("fin de programme...");
                        Console.ReadKey();
                        fin = true;
                        break;
                    default:
                        Console.WriteLine("\nchoix non valide => faites un autre choix....");
                        break;
                }
            } while (!fin);

        }


        /// <summary>
        /// Cette méthode permet d'importer un rapport de journée de crime et de le stocker dans un dataset
        /// la méthode demande d'abord à l'utilisateur de rentrer le nom du fichier
        /// Ensuite la fonction extrait les données du fichier xml dans un dataset et insère les données du crime dans la base de données
        /// </summary>
        static void InsertionJournee()
        {
            Console.WriteLine("\n1 => Importer une journée de crimes (question 1.1)");
            Console.WriteLine("-----------------\n");

            //création du canal de communication avec mysql
            MySqlConnection maconnection = new MySqlConnection(connectionString);
            maconnection.Open();

            //déclaration du dataset et du lecteur de fichier xml
            DataSet dataset = new DataSet();
            XmlReader lecteur;

            //declaration des différents attributs et de la requete sql
            string date = "";
            string borough = "";
            string coord_x = "";
            string coord_y = "";
            string desc_crime = "";
            string desc_specificite = "";
            string jurisdiction = "";
            string requete = "";

            //déclaration du fichier
            Console.WriteLine("Saisissez le nom du fichier : ");
            string fichier = Console.ReadLine();

            XmlReaderSettings parametres = new XmlReaderSettings();
            parametres.DtdProcessing = DtdProcessing.Parse;
            lecteur = XmlReader.Create(fichier, parametres);
            dataset.ReadXml(lecteur); //lit le fichier xml
            
            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                //extraction des données du fichier xml et insertion dans le dataset
                date = dataset.Tables[0].Rows[0].ItemArray[0].ToString();
                borough = dataset.Tables[0].Rows[i].ItemArray[1].ToString();
                coord_x = dataset.Tables[0].Rows[i].ItemArray[2].ToString();
                coord_y = dataset.Tables[0].Rows[i].ItemArray[3].ToString();
                desc_crime = dataset.Tables[0].Rows[i].ItemArray[4].ToString();
                desc_specificite = dataset.Tables[0].Rows[i].ItemArray[5].ToString();
                jurisdiction = dataset.Tables[0].Rows[i].ItemArray[6].ToString();

                //commande sql pour insérer des données dans la table jurisdiction
                MySqlCommand command1 = maconnection.CreateCommand();
                requete = "INSERT INTO Jurisdiction (name) VALUES ('" + jurisdiction + "')";
                command1.CommandText = requete;
                command1.ExecuteNonQuery();
                //MySqlCommand commande = new MySqlCommand(command.CommandText, maconnection);

                //commande sql pour insérer des données dans la table Crime_description
                MySqlCommand command2 = maconnection.CreateCommand();
                requete = "INSERT INTO Crime_description (description, desc_specificity) VALUES ('" + desc_crime + "','" + desc_specificite + "')";
                command2.CommandText = requete;
                command2.ExecuteNonQuery();

                //commande sql pour relever l'ID associée au tuple inséré juste avant dans la table Jurisdiction
                MySqlCommand command3 = maconnection.CreateCommand();
                requete = "SELECT MAX(id) AS id FROM Jurisdiction";
                command3.CommandText = requete;
                MySqlDataReader reader = command3.ExecuteReader();
                reader.Read();
                string jurisdiction_id = reader["id"].ToString();
                reader.Close();

                //commande sql pour relever l'ID associée au tuple inséré juste avant dans la table Crime_description
                MySqlCommand command4 = maconnection.CreateCommand();
                requete = "SELECT MAX(id) AS id FROM Crime_description";
                command4.CommandText = requete;
                MySqlDataReader reader_bis = command4.ExecuteReader();
                reader_bis.Read();
                string crime_id = reader_bis["id"].ToString();
                reader_bis.Close();

                //insertion des données du fichier xml et avec les ID relevés, dans la table Crime
                MySqlCommand command5 = maconnection.CreateCommand();
                requete = "INSERT INTO Crime (date, borough, coord_X, coord_Y, crime_description_id, jurisdiction_id) VALUES ('" + date + "','" + borough + "','" + coord_x + "','" + coord_y + "','" + crime_id + "','" + jurisdiction_id + "')";
                command5.CommandText = requete;
                command5.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Cette méthode demande de saisir une date pour relever les informations sur le crime commis ce jour-là et crée un rapport de police dans un fichier xml
        /// </summary>
        static void ExporterBilanJournalier()
        {
            Console.WriteLine("\n2 => Exporter le bilan journalier (question 1.2)");
            Console.WriteLine("-----------------\n");

            Console.WriteLine("Saisissez la date du crime :");
            string date = Console.ReadLine();

            MySqlConnection maconnection = new MySqlConnection(connectionString);
            maconnection.Open();
            //commande sql pour relever les informations du crime commis à la date saisie
            MySqlCommand command = maconnection.CreateCommand();
            string request = "SELECT Crime.borough,Crime.date,Crime_description.description, Crime_description.desc_specificity, Jurisdiction.name FROM Crime JOIN Crime_description ON Crime.crime_description_id = Crime_description.id JOIN Jurisdiction ON Crime.jurisdiction_id = Jurisdiction.id WHERE Crime.date ='" + date + "'";
            command.CommandText = request;
            MySqlCommand commande = new MySqlCommand(command.CommandText, maconnection);

            //
            DataSet dataset = new DataSet("resultats");
            MySqlDataAdapter sqladapter = new MySqlDataAdapter(commande);

            //remplit l'adaptateur du dataset 
            sqladapter.Fill(dataset);
            dataset.Tables["table"].TableName = "Crime"; //extraire les donnés de la table dans le dataset
            maconnection.Close();
            sqladapter.Dispose();  //close l'adaptateur
            dataset.WriteXml("NPY-date.xml");   //crée ou complète le fichier NPY-date.xml
            Console.WriteLine("Le contenu a bien été exporté");

            Console.WriteLine("-----------------\n\n");
        }


        /// <summary>
        /// Cete méthode demande à l'utilisateur de saisir toutes les informations d'un crime pour les rentrer dans la base de données
        /// </summary>
        static void SaisirUnCrime()
        {
            Console.WriteLine("\n3 => Saisie d'un crime (question 2.1)");
            Console.WriteLine("-----------------\n");
            Console.WriteLine("Saisissez la date du crime");
            string date = Console.ReadLine();
            Console.WriteLine("Saisissez le nom du quartier où le crime a été commis");
            string borough = Console.ReadLine();
            Console.WriteLine("Saisissez les coordonnées X et Y du lieu du crime");
            Console.Write("X : ");
            string CoordX = Console.ReadLine();
            Console.Write("Y : ");
            string CoordY = Console.ReadLine();
            Console.WriteLine("Saisissez la description du crime");
            string desc_crime = Console.ReadLine();
            Console.WriteLine("Saisissez la description spécifique du crime");
            string desc_specificite = Console.ReadLine();
            Console.WriteLine("Saisissez la juridiction associée au crime");
            string jurisdiction = Console.ReadLine();

            MySqlConnection maconnection = new MySqlConnection(connectionString);
            maconnection.Open();

            //commande sql pour insérer la juridiction relevée dans la table Jurisdiction
            MySqlCommand command1 = maconnection.CreateCommand();
            string request = "INSERT INTO Jurisdiction (name) VALUES ('" + jurisdiction + "')";
            command1.CommandText = request;
            command1.ExecuteNonQuery();
            
            //commande sql pour insérer la description du crime dans la table Crime_description
            MySqlCommand command2 = maconnection.CreateCommand();
            request = "INSERT INTO Crime_description (description, desc_specificity) VALUES ('" + desc_crime + "','" + desc_specificite + "')";
            command2.CommandText = request;
            command2.ExecuteNonQuery();

            //commande sql pour relever l'id de la juridiction insérée juste avant dans la table Jurisdiction
            MySqlCommand command3 = maconnection.CreateCommand();
            request = "SELECT MAX(id) AS id FROM Jurisdiction";
            command3.CommandText = request;
            MySqlDataReader reader1 = command3.ExecuteReader();
            reader1.Read();
            string juridiction_id = reader1["id"].ToString();
            reader1.Close();

            //commande sql pour relever l'id de la description insérée juste avant dans la table Crime_description
            MySqlCommand command4 = maconnection.CreateCommand();
            request = "SELECT MAX(id) AS id FROM Crime_description";
            command4.CommandText = request;
            MySqlDataReader reader2 = command4.ExecuteReader();
            reader2.Read();
            string crime_id = reader2["id"].ToString();
            reader2.Close();

            //commande sql pour insérer toutes les données du crime dans la table Crime
            MySqlCommand command5 = maconnection.CreateCommand();
            request = "INSERT INTO Crime (date, borough, coord_X, coord_Y, crime_description_id, jurisdiction_id) VALUES ('" + date + "','" + borough + "','" + CoordX + "','" + CoordY + "','" + crime_id + "','" + juridiction_id + "')";
            command5.CommandText = request;
            command5.ExecuteNonQuery();
            maconnection.Close();
            Console.WriteLine("-----------------\n\n");
        }


        /// <summary>
        /// Cette méthode demande de saisir une date et affiche le nombre de crimes commis à la date rentrée par quartiers et par descriptions à cette date
        /// </summary>
        static void CrimesParQuartier()
        {
            Console.WriteLine("\n4 => Le nombre de crimes par quartier et par catégorie (question 3.1)");
            Console.WriteLine("-----------------\n");
            Console.WriteLine("Saisissez une date");
            string date = Console.ReadLine();

            MySqlConnection maconnection = new MySqlConnection(connectionString);
            maconnection.Open();

            //commande sql pour relever le nombre de crimes commis à la date rentrée par quartiers et par descriptions commis à la date relevée
            MySqlCommand command = maconnection.CreateCommand();
            string requete = "select borough, Crime_description.description as description , count(*) as c from crime join crime_description on crime_description.id = crime.crime_description_id where date='" + date + "' group by  borough, description";
            command.CommandText = requete;
            
            //execution de la commande et affichage du résultat
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string tuple = reader["borough"].ToString() + " " + reader["description"].ToString() + " " + reader["c"].ToString();
                Console.WriteLine(tuple);
            }
            reader.Close();
            maconnection.Close();

            Console.WriteLine("-----------------\n\n");
        }


        /// <summary>
        /// Cette méthode demande de saisir un mois d'une année pour y afficher la liste des crimes de type grand larcin commis ce mois-là
        /// </summary>
        static void RecapitulatifMensuel()
        {
            Console.WriteLine("\n5 => Récapitulatif pour un mois (question 3.2)");
            Console.WriteLine("-----------------\n");
            Console.WriteLine("Saisissez une date au format MM/yyyy :");
            string date = Console.ReadLine();

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            //commande sql pour relever la liste des crimes de type 'grand larcin' (grand larceny) pour le mois saisi
            MySqlCommand command = connection.CreateCommand();
            string requete = "select borough,date, description from Crime join crime_description on crime_description.id = crime.crime_description_id where crime_description.description like 'grand larceny%' and date like '%" + date + "' group by  borough";
            command.CommandText = requete;
            
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string tuple = reader["borough"].ToString() + " " + reader["date"].ToString() + " "  + reader["description"].ToString();
                Console.WriteLine(tuple);
            }
            Console.WriteLine("-----------------\n\n");
        }


        /// <summary>
        /// Cette méthode affiche pour chaque mois la répartition par quartier en % des crimes
        /// pour chaque mois, le programme relève le nombre de crimes de chaque quartier et compare ce nombre avec le nombre total de crimes du mois
        /// </summary>
        static void EvolutionMoisParMois()
        {
            Console.WriteLine("\n6 => Evolution mois par mois (question 3.3)");
            Console.WriteLine("-----------------\n");

            MySqlConnection maconnection = new MySqlConnection(connectionString);
            maconnection.Open();

            
            double taux = 0.0;
            int mois = 1;
            
            while (mois <= 12)
            {
                int index = 0;
                List<double> liste = new List<double>();
                List<string> quartier = new List<string>();
                //commande sql pour relever le nombre de crimes grands larcins par quartiers pour le mois sélectionné
                MySqlCommand command1 = maconnection.CreateCommand();
                string requete = "select count(*) as c, borough,date, Crime_description.description as description from crime join crime_description on crime_description.id = crime.crime_description_id where description like 'grand larceny%' and date like '%" + mois + "/2012' group by  borough, crime_description.description";
                command1.CommandText = requete;
                
                MySqlDataReader reader1 = command1.ExecuteReader();
                Console.Write(mois + "/2012");
                Console.Write(" ");
                while (reader1.Read())
                {
                    //ajout du nombre de crimes 
                    liste.Add(Convert.ToInt32(reader1["c"].ToString()));

                    //ajout du nom du quartier
                    quartier.Add(reader1["borough"].ToString());
                }
                reader1.Close();

                //commande sql 
                MySqlCommand command2 = maconnection.CreateCommand();
                string requete2 = "select count(*) as c from crime where date like '%" + mois + "/2012'";
                command2.CommandText = requete2;
                
                MySqlDataReader reader2 = command2.ExecuteReader();
                reader2.Read();
                double nombre = Convert.ToInt32(reader2["c"].ToString());
                //affichage pour le mois sélectionné de la répartition par quartier des crimes
                Console.WriteLine("pour le mois " + mois);
                while (index < liste.Count)
                {
                    taux = (liste[index] / nombre) * 100;
                    string affichage = "Quartier concerné : " + quartier[index] + " Taux de criminalité : " + taux + "%";
                    Console.WriteLine(affichage);
                    index++;
                }
                reader2.Close();
                mois++;
            }

            maconnection.Close();
            Console.WriteLine("-----------------\n\n");
        }


        /// <summary>
        /// Cette méthode renvoie le quartier avec le plus de crimes
        /// </summary>
        static void PalmaresAnnuel()
        {
            Console.WriteLine("\n7 => Palmarès annuel (question 4.1)");
            Console.WriteLine("-----------------\n");

            MySqlConnection maconnection = new MySqlConnection(connectionString);
            maconnection.Open();

            //commande sql pour relever le nombre de crimes de chaque quartier en 2012
            MySqlCommand command1 = maconnection.CreateCommand();
            string requete = "select count(*) as c,borough from crime where date like '%/2012' group by borough";
            command1.CommandText = requete;
            
            MySqlDataReader reader1 = command1.ExecuteReader();

            List<double> liste = new List<double>();
            List<string> liste_quartiers = new List<string>();

            while (reader1.Read())
            {
                liste.Add(Convert.ToInt64(reader1["c"].ToString()));

                liste_quartiers.Add(reader1["borough"].ToString());
            }
            reader1.Close();

            double maximum = liste.Max();
            int indexMax = liste.IndexOf(maximum);
            string affichage = "avec " + maximum + " crimes en 2012, le quartier de " + liste_quartiers[indexMax] + " est le plus dangereux de la ville, faites attention si vous y allez";
            Console.WriteLine(affichage);
            
            //commande sql qui pour chaque type de crime relève le nombre de fois où il a été commis dans le quartier le plus dangereux de la ville
            MySqlCommand command2 = maconnection.CreateCommand();
            string requete2 = "select count(*) as c, borough, crime_description.description from crime join Crime_description on crime.crime_description_id = Crime_description.id where borough='" + liste_quartiers[indexMax] + "' group by crime_description.description";
            command2.CommandText = requete2;
            MySqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                string affichage2 = reader2[2].ToString() + " " + reader2["c"].ToString();
                Console.WriteLine(affichage2);
            }
            reader2.Close();
            maconnection.Close();

            Console.WriteLine("-----------------\n\n");
        }


        /// <summary>
        /// Cette méthode affiche la répartition par mois en % des crimes de l'année 2012
        /// </summary>
        static void VotreFonction()
        {
            Console.WriteLine("\n8La fonction de votre choix (question 5.1)");
            Console.WriteLine("-----------------\n");
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command0 = connection.CreateCommand();
            string requete0 = "select count(*) as c from crime where date like '%2012'";
            command0.CommandText = requete0;
            MySqlDataReader reader0 = command0.ExecuteReader();
            reader0.Read();
            double total = Convert.ToInt32(reader0["c"].ToString());
            Console.WriteLine(total);
            List<int> liste = new List<int>();
            int mois = 1;
            reader0.Close();
            double taux = 0.0;
            while (mois <= 12)
            {
                MySqlCommand command1 = connection.CreateCommand();
                string requete1 = "select count(*) as c, date from crime where date like '%" + mois + "/2012'";
                command1.CommandText = requete1;
                MySqlDataReader reader1 = command1.ExecuteReader();
                reader1.Read();
                Console.Write(mois + "/2012");
                Console.Write(" ");
                int nombre = Convert.ToInt32(reader1["c"].ToString());
                liste.Add(nombre);
                reader1.Close();
                taux = (nombre / total) * 100;
                string affichage = "le mois " + mois + " : " + nombre + " crimes, soit " + taux + "% des crimes de l'année";
                Console.WriteLine(affichage);
                mois++;
            }
            int max = liste.Max();
            int indexMax = liste.IndexOf(max) + 1;
            string affichage2 = "avec " + max + " crimes, le mois " + indexMax + " est le plus dangereux";
            Console.WriteLine(affichage2);

            Console.WriteLine("-----------------\n\n");
        }
    }
}
