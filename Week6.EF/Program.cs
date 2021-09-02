using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Week6.EF.Core.Models;
using Week6.EF.EF;

namespace Week6.EF
{
    class Program
    {
        //Istanza di KnightsContext:
        private static KnightsContext _knightsContext = new KnightsContext();
        static void Main(string[] args)
        {
            _knightsContext.Database.EnsureCreated(); //per assicurarsi dell'esistenza del db.
                                                      //Se non esiste, lo crea.
                                                      //(questo metodo si usa nei test)

            //Recuperiamo tutti i cavalieri 
            //Console.WriteLine("Prima dell'aggiunta:");
            //FetchKnights();

            //Aggiungiamo un cavaliere 
            //AddKnight();

            //Recuperiamo tutti i cavalieri dopo l'aggiunta
            //Console.WriteLine("Dopo dell'aggiunta:");
            //FetchKnights();

            //Aggiunta tipi diversi
            // AddVariousTypes();

            //aggiungere cavaliere con ascia o altro
            //AddKnightWithWeapons();
            //AddNewWeaponToExistingKnigth();


            //Filtrare tutti i cavalieri con nome Telman

            //Recuperare il primo con nome Telman


            //Recuperare il cavaliere con ID (2 modi)

            //Update Cavaliere
            //GetAndUpdateCavaliere();

            //Eliminare Cavaliere
            //GetAndDelete();

            //---------------------------------------------------------
            //DATI CORRELATI
            //EagerLoagingKnightsWithWeapons();
        }
        #region - La parte uno fetch,Add,Delete,Update
        private static void FetchKnights()
        {
            //Query LINQ - per reccuperare tutti cavalieri dal DB
            var knights = _knightsContext.Knights.ToList();

            //Stampiamo il numero di records cavalieri nella tabella db
            Console.WriteLine($"Il numero di cavalieri è: {knights.Count}");

            //Stampiamo i nomi dei cavalieri nel db
            foreach (var k in knights)
                Console.WriteLine(k.Name);

            //poco performante, potrebbe creare problemi, meglio spezzare
            //foreach (var k in _knightsContext.Knights)
            //{
            //    Console.WriteLine(k.Name);
            //}


        }

        private static void AddKnight()
        {
            var newKnight = new Knight { Name = "Boar" }; //avrà una lista di armi vuota

            _knightsContext.Knights.Add(newKnight);

            _knightsContext.SaveChanges();

        }
        private static void AddVariousTypes()
        {
            _knightsContext.AddRange(
                new Knight { Name = "Driaco" },
                new Battle { Name = "Cassel" }
                );
            _knightsContext.SaveChanges();
        }

        private static void GetByName()
        {
            //accedo ai cavalieri al dbset, varo una lista dei cavalieri con questo nome
            var knights = _knightsContext.Knights.Where(k => k.Name == "Telman").ToList();
        }

        //se il nome viene dal'utente
        private static void FilterByName()
        {
            var name = "Driaco"; // ora lo scrivo cosí pero normalmente lo avrei passato in FilterByName(string name)
            var knihts = _knightsContext.Knights.Where(k => k.Name == name).ToList();
        }

        //Reccuperare il primo con un certo nome
        private static void GetFirstByName()
        {
            var name = "Telman";

            //fisrt si usa per trovare il primo con il nome, cognome .. ,Find si usa per trovare con ID
            //var knight = _knightsContext.Knights.Where(k => k.Name == name).First();
            //se non trova niente da u nvalore di dafault - null e possiamo fare controllo
            var knight = _knightsContext.Knights.Where(k => k.Name == name).FirstOrDefault();

        }

        public static void GetKnightById()
        {
            var knight = _knightsContext.Knights.FirstOrDefault(k => k.Id == 3);
        }

        public static void GetKnightById_Find()
        {
            //cosí va direttamente cercare un cavaliere con id 3
            var knight = _knightsContext.Knights.Find(3);
        }

        public static void GetAndUpdateCavaliere()
        {
            var knight = _knightsContext.Knights.FirstOrDefault();
            knight.Name = "Valfred";

            //per riportare cambiamenti al DB
            _knightsContext.SaveChanges();
        }

        //Cancellare un cavaliere tramitte suo ID
        public static void GetAndDelete()
        {
            var knight = _knightsContext.Knights.Find(5); // utente sceglie quale cavaliere eliminare
            _knightsContext.Knights.Remove(knight);
            _knightsContext.SaveChanges();
        }

        private static void AddKnightWithWeapons()
        {
            var knight = new Knight
            {
                Name = "Tusman",
                Weapons = new List<Weapon>
                {
                    new Weapon
                    {
                        Description = "Scimitarra"
                    }
                }

            };

            _knightsContext.Knights.Add(knight);
            _knightsContext.SaveChanges();
        }

        //Metodo per aggiungere un'arma a un cavaliere esistente
        private static void AddNewWeaponToExistingKnigth()
        {
            var knight = _knightsContext.Knights.FirstOrDefault();
            knight.Weapons.Add(new Weapon
            {
                Description = "Ascia"
            });
            _knightsContext.SaveChanges();
        }
        #endregion la parte uno 

        //Carricamento dei dati correlati
        private static void EagerLoagingKnightsWithWeapons()
        {
            var knights = _knightsContext.Knights.ToList(); // fa solo tolist di cavalieri non porta dietro le armi

            var knightsWtihWeapons = _knightsContext.Knights.Include(k => k.Weapons).ToList(); //porta anche le army con sé, quindi le cose legate a sé
        }

        //Recuperare i cavalieri e solo le armi con descrizione Ascia .INCLUDE
        //ThenInclude - Blog-Post-Tag , dal post includo anche i Tags
        private static void EagerLoagingKnightsWthWeapons_Filter()
        {
            var knightsWithAscia = _knightsContext.Knights.Include(k => k.Weapons.Where(w => w.Description == "Ascia")).ToList();

        }

        //Query sul dati correlati - le proiezione- che dati di risultati mi aspetto .SELECT

        //solo ID e NOME del cavaliere - definisco un nuovo tipo che nasce e muore in quel metodo, non posso portarlo fuori -TIPO ANONIMO
        private static void IdAndName()
        {
            var knights = _knightsContext.Knights.Select(k => new { k.Id, k.Name }).ToList();
        }

        //se ho bisogno di un tipo che voglio restituire ---------------
        public struct IdAndNome
        {
            public int Id;
            public string Name;
            public IdAndNome(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public static void IdAndName_Struct()
        {
            var idAndName = _knightsContext.Knights.Select(k => new IdAndNome(k.Id, k.Name)).ToList();
        }
        //Struct ---------------------------------------------------------

        //EXPLICIT LOADING
        //usiamo metodo ENTRY,collection o reference posso usare insieme

        private static void ExplicitLoading()
        {
            var knights = _knightsContext.Knights.Find(3);
            _knightsContext.Entry(knights).Collection(k => k.Weapons).Load(); // Entry restituisce antitá,Collection recupera una arma associate al cavaliere scelto
            _knightsContext.Entry(knights).Reference(k => k.Horse).Load(); // con REFERENCE recupero il Horse , se ce una lista dovrei fare proprio una nuova Query
        }
        
    }
}
