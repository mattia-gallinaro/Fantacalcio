//nickname: matgal
//Date: 14/11/2021
using System;
using System.IO;
using System.Collections.Generic;

namespace Fantacalcio
{
    class Calciatore
    {
        //attributi della classe Calciatore , ciascuno di essi ha il metodo get ed il metodo set per modificare e leggere il valore degli attributi anche all'esterno della classe
        public string nome_e_cognome { get; set; }
        public string ruolo { get; set; }
        public string sqaudra { get; set; }
        public int costo { get; set; }
        public int PunteggioPartita { get; set; }
        public bool gia_assegnato { get; set; }
        public Calciatore()//costruttore della classe Calciatore
        {
        }
        public List<Calciatore> GetCalciatori(string[] array)//prende in input l'array contente tutti i calciatori
                                                             //creo la lista calciatorifile di tipo Calciatori
                                                             //divide ciascun elemnto di array in array calciatore tramite il metodo .Split();
                                                             //per ciascun ciclo assegno i dati di arraycalciatore nella  
        {
            List<Calciatore> calciatorifile = new List<Calciatore>();//creo la lista calciatorifile di tipo Calciatori
            for (int i = 0; i < array.Length; i++)//si ripete fino a quando non viene salvato l'intero contenuto di array nella lista calciatorifile
            {
                string[] arraycalciatore = array[i].Split(',');//per la riga di array in posizione i, divido gli elementi per il carattere ','
                calciatorifile.Add(new Calciatore());//ad ogni ciclo viene creata una nuova linea in cui inserire gli attributi per ciascun calciatore
                calciatorifile[i].nome_e_cognome = arraycalciatore[0];//assegna alla variabile nome_e_cognome del calciatore appena inserito nella lista calciatorifile, il valore di arraycalciatore[0]
                calciatorifile[i].ruolo = arraycalciatore[1];//assegna alla variabile nome_e_cognome del calciatore appena inserito nella lista calciatorifile, il valore di arraycalciatore[0]
                calciatorifile[i].sqaudra = arraycalciatore[2];//pone a 0 la variabile PunteggioPartita del calciatore appena inserito nella lista calciatorifile
                calciatorifile[i].PunteggioPartita = 0;//pone a 0 la variabile PunteggioPartita del calciatore appena inserito nella lista calciatorifile
                calciatorifile[i].costo = 0;//pone a 0 il prezzo del calciatore appena inserito nella lista calciatorifile
                calciatorifile[i].gia_assegnato = false;//pone a false la variabile gia_asseganto del calciatore appena inserito nella lista calciatorifile
            }
            
            return calciatorifile;//ritorno la lista calciatorifile
        }
        public int ControlloCalciatoreAsta(ref List<Calciatore> calciatori, string risposta)//prende in input la lista calciatori della classe main andando ad agire direttamente sui valori di essa, e la stringa con il nome del giocatore inserito dall'utente per l'asta
        {
            bool controllo = false;//variabile booleana
            for(int i = 0; i < calciatori.Count && !controllo; i++)
            {
                if(calciatori[i].nome_e_cognome == risposta)//controlla che il nome inserito dall'utente corrisponde alla variabile nome_e_cognome dell'elemento della lista calciatore in indice i
                {
                    if (calciatori[i].gia_assegnato)//controlla se il calciatore da mettere all'asta è già stato comprato
                    {
                        return -1;//ritorna -1 perchè il calciatore richiesto per l'asta è già stato comprato
                    }
                    else
                    {
                        controllo = true;
                        return i;//ritorna l'indice del calciatore selezionato della lista calciatori
                    }
                }
            }
            return -1;//ritorna -1 perché il calciatore inserito dall'utente non è presente nella lista calciatori
        }
            
    }
    class Giocatori
    {
        //attributi della classe Giocatori , ciascuno di essi ha il metodo get ed il metodo set per modificare e leggere il valore degli attributi anche all'esterno della classe
        public string nome { get; set; }
        public int punteggio { get; set; }
        public int punteggio_totale { get; set; }
        public int crediti { get; set; }
        //liste di tipo Calciatore per contenere la Rosa e la formazione per ciascun calciatore
        public List<Calciatore> Rosa = new List<Calciatore>();
        public List<Calciatore> Formazione = new List<Calciatore>();
        public Giocatori()//costruttore della classe Giocatori
        {

        }
        public bool ControlloNomeInserito(string risposta, string[] nomigiocatori)//prende in input il nome inserito dall'utente e l'array nomigiocatori che contiene i nomi dei giocatori
                                                                                  //serve per il controllo dei nomi inseriti dall'utente per i giocatori affinché siano diversi
        {
            bool verifica = false;
            if(risposta == "" || risposta.Contains(","))//controlla se il nome inserito dall'utente contiene spazi oppure delle virgole
            {
                verifica = true;//pone a true la variabile verifica
                return verifica;//ritorna il valore della variabile verifica
            }
            if (nomigiocatori.Length > 0)//controlla se la lunghezza dell'array è maggiore di 0
                                         //se sì controllo eseguo il codice presente nell'if
            {
                for (int i = 0; i < nomigiocatori.Length; i++)
                {
                    if(risposta == nomigiocatori[i])//controlla se il contenuto di risposta corrisponde al contenuto di nomigiocatori all'indice i
                    {
                        verifica = true;//assegna true alla variabile verifica
                    }
                }
                return verifica;//ritorna il valore di verifica
            }
            else//la lunghezza di nomigiocatori è 0 per cui non può controllare il contenuto di risposta con nessun nome di nomigiocatori
            {
                return verifica;//ritorna il valore di verifica quindi false
            }
        }
        public int ControlloPrezzoInserito()//metodo che controlla il prezzo inserito nell'asta dal giocatore
        {
            try//utilizza "try" per controllare il valore che l'utente inserisce come risposta
               //utilizza "catch" per gestire gli errori come nel caso in cui l'utente inserisce da tastiera qualcosa che non sia un numero
            {
                int valore = int.Parse(Console.ReadLine());//converte la risposta dell'utente in intero e lo assegna alla variabile valore
                if(valore < 0 || valore > crediti)//controllo che il valore inserito dall'utente sia minore di 0 e se sia maggiore dei crediti del calciatore
                {
                    return 111;//ritorna 111 che supera il limite consentito dell'asta
                }
                else
                {
                    return valore;//ritorna il valore inserito dall'utente
                }     
            }
            catch
            {
                return 111;//ritorna 111 che supera il limite consentito dell'asta
            }
        }
        public int Asta(int[] prezzi)//riceve in input l'array contenente le offerte dei giocatori per l'asta 
        {
            int[] indici = new int[0];//array in cui vengono inseriti gli indici dei giocatori con le puntate massime
            int max = -1;//variabile in cui contenere la puntata massia
            for(int i = 0; i< prezzi.Length; i++)
            {
                if(max < prezzi[i])//controlla se il valore di max sia minore di prezzi a livello i
                {// se ciò si verifica
                    max = prezzi[i];//assegna il valore di prezzi a livello i alla variabile max
                }        
            }
            int m = 0;//variabile per il conteggio degli utenti con lo stesso 
            for(int j = 0; j < prezzi.Length; j++)//ciclo for che serve per contare quanti giocatori hanno inserito come offerta nell'asta, max
            {
                if(prezzi[j] == max)//controlla che il prezzo in posizione j corrisponda a max,
                                    //se ciò si verifica allora il valore di j viene assegnato all'array indici in posizione m
                {
                    Array.Resize(ref indici, indici.Length + 1);//aumenta la grandezza dell'array indici di 1
                    indici[m] = j;
                    m++;//aumenta m di 1
                }
            }
            if(indici.Length == 1)//controlla se la grandezza dell'array indici è 1 perché vuol dire che un solo giocatore ha fatto l'offerta con valuta pari a max
            {
                return indici[0];//ritorna il valore di indici a livello 0, quindi ritorna l'indice di chi ha messo la puntata massima
            }
            else
            {
                Random random = new Random();//crea un'instanza della classe Random
                int numero_generato = random.Next(0, indici.Length);//invoca la funzione Next della classe Random passando come limiti 0 e la grandezza dell'array indici.Length e assegna il valore generato a numero_generato
                return indici[numero_generato];//ritorna il valore di indici a livello numero_generato, quindi ritorna l'indice di chi ha messo la puntata massima
            }
        }
        public bool ControlloRose(ref List<Giocatori> giocatoris)//metodo che, ricevendo in input il riferimento dell'array giocatori, conta se tutti i giocatori hanno almeno 11 calciatori nella propria rosa
        {
            bool controllo = false;//variabile booleana
            for (int i = 0; i < giocatoris.Count; i++)//ciclo che si ripete per tutti i giocatori presenti nella lista giocatoris
            {
                if (giocatoris[i].Rosa.Count < 11)//controlla se il numero di calciatori presenti nella rosa di ciascun giocatore sia minore di 11
                {
                    controllo = true;//pone a true il valroe di controllo
                }
            }
            return controllo;//ritorna il valore di controllo
        }
        public void AssegnazioneRosa(ref List<Calciatore> calciatores, int indice_c,int prezzo)//metodo che serve per assegnare il calciatore comprato nell'asta al giocatore che l'ha ottenuto
        {
            Rosa.Add(new Calciatore());//aggiunge un nuovo elemento alla lista Rosa del giocatore
            Rosa[Rosa.Count-1].nome_e_cognome = calciatores[indice_c].nome_e_cognome;//
            Rosa[Rosa.Count-1].sqaudra = calciatores[indice_c].sqaudra;
            Rosa[Rosa.Count-1].ruolo = calciatores[indice_c].ruolo;
            Rosa[Rosa.Count-1].costo = prezzo;
            calciatores[indice_c].gia_assegnato = true;
            crediti -= Rosa[Rosa.Count-1].costo;

        }
        public int ControlloGiocatoreFormazione(ref List<Calciatore> calciatores, string risposta)//metodo per controllare se il calciatore che il giocatore vuole inserire nella formazione gli appertiene o è già stato assegnato 
        {
            int n = -1;//variabile che contiene l'indice del calciatore della rosa da assegnare nelle formazione
            for(int i = 0; i < calciatores.Count; i++)//ciclo for che si ripete fino a quando 
            {
                if(calciatores[i].nome_e_cognome == risposta)
                {
                    if (!calciatores[i].gia_assegnato)
                    {
                        n = i;
                    }
                }        
            }
            return n;
        }
            
        public void AssegnazioneFormazione(ref List<Calciatore> calciatores, int indice_c)
        {
            Formazione.Add(new Calciatore());
            Formazione[Formazione.Count - 1].nome_e_cognome = calciatores[indice_c].nome_e_cognome;
            Formazione[Formazione.Count - 1].sqaudra = calciatores[indice_c].sqaudra;
            Formazione[Formazione.Count - 1].ruolo = calciatores[indice_c].ruolo;
            Formazione[Formazione.Count - 1].costo = calciatores[indice_c].costo;
            calciatores[indice_c].gia_assegnato = true;
        }
        public void CalcolaPunteggio()//metodo che aumenta il punteggio di ciascun giocatore della lista giocatori
        {
            for(int i = 0; i < Formazione.Count; i++)
            {
                punteggio += Formazione[i].PunteggioPartita;
            }
            punteggio_totale += punteggio;
        }
    }
    
    class Partita
    {
        public Partita()
        {

        }
        public int[] GenerazionePartita(int quantita)
        {
            int[] array = new int[quantita];//array che contiene gli indici da generare per le partite
            Random random = new Random();//crea un'istanza della classe Random
            int numero_generato = 0;//variabile che contiene il numero generato
            bool controllo = false;//variabile booleana usata per il controllo nella generazione di doppioni
            for (int i = 0; i < array.Length; i++)//for che si ripete per il numero di squadre inserite dall'utente
            {
                numero_generato = random.Next(0, quantita);//viene richiamata la funzione Next della classe Random dando come parametri 0 ed quantità
                if (i >= 1)//inizia a controllare appena avrà generato il secondo numero
                {
                    for (int j = 0; j < i; j++)//for usato per il controllo dei doppioni
                                               //si ripete fino a quando j raggiunge il valore di i
                    {
                        if (array[j] == numero_generato)//controlla che il numero appena generato non corrisponda ad un numero già presente in array
                        {
                            controllo = true;//se è un doppione allora cambio il valore di controllo e lo rendo true
                        }

                    }
                }
                else//se è il primo numero ad esse generato
                {
                    array[i] = numero_generato;
                }
                if (controllo)//se si tratta di un doppione fa ripetere la generazione del numero 
                {
                    i--;//diminuisce il valore di i di 1 per far ripetere la generazione del numero
                    controllo = false;//riporto il valore di controllo a false sennò non terminerebbe mai la generazione dei numeri
                }
                else//se il numero generato non è doppione
                {
                    array[i] = numero_generato;//salvo il numero appena generato in array nella posizione i
                }
            }
            return array;//ritorno l'array con gli indici delle partite
        }
        public int AssegnazionePunteggioCalciatore()//metodo per assegnare un punteggio random a ciascun giocatore
        {
            Random random = new Random();//crea l'istanza random della classe Random 
            int numero_generato = random.Next(2, 12);//invoca il metodo Next della classe Random e genera un valore random compreso tra 2 e 12
            return numero_generato;//ritorna il numero generato
        }
        /*public void ClassificaProvvisoria(ref List<Giocatori> giocatori)//metodo per ordinare i giocatori in base al punteggio
        {
            bool controllo;//variabile booleana
            do
            {
                controllo = false;//riassegna false a controllo sennò continuerebbe all'infinito
                for (int i = 0; i < (giocatori.Count - 1); i++)//il ciclo si ripete per la quantità di squadre presenti nella lista bubble diminuito di 1
                {
                    if (giocatori[i].punteggio < giocatori[i + 1].punteggio)//controlla se il punteggio della squadra in posizione i della lista bubble sia minore del punteggio della squadra in posizione successiva ad i nella lista bubbble
                    {
                        giocatori.Reverse(i, 2);//scambio i due elementi della lista 
                        controllo = true;
                    }
                }
            } while (controllo);//si ripete fino a quando il valore della variabile booleana corrisponde a false
        }*/
        public void ClassificaFinale(ref List<Giocatori> giocatori)//riceve in input l'array giocatori e riordina il 
        {
            bool controllo;//variabile booleana
            do
            {
                controllo = false;//riassegna false a controllo sennò continuerebbe all'infinito
                for (int i = 0; i < (giocatori.Count - 1); i++)//il ciclo si ripete per la quantità di squadre presenti nella lista bubble diminuito di 1
                {
                    if (giocatori[i].punteggio_totale < giocatori[i + 1].punteggio_totale)//controlla se il punteggio della squadra in posizione i della lista bubble sia minore del punteggio della squadra in posizione successiva ad i nella lista bubbble
                    {
                        giocatori.Reverse(i, 2);//scambio i due elementi della lista 
                        controllo = true;
                    }
                }
            } while (controllo);//si ripete fino a quando il valore della variabile booleana corrisponde a false
        }
        public int GestionePartita(ref List<Giocatori> giocatores, int indice_1, int indice_2)//metodo per la gestione randomica delle partite
                                                                                              //riceve come parametri la lista dei giocatori e gli indici dei giocatori che si sfidano nella partita
        {
            int n;//variabile in cui viene salvato l'esito della partita
            for(int i  = 0; i< giocatores[indice_1].Formazione.Count; i++)//ciclo for che si ripete per i giocatori della formazione del primo giocatore
            {
                giocatores[indice_1].Formazione[i].PunteggioPartita = AssegnazionePunteggioCalciatore();//invoca la funzione AssegnazionePunteggioCalciatore, per ogni calciatore presente nell formazione, e gli assegna il valore di ritorno
            }
            for (int i = 0; i < giocatores[indice_2].Formazione.Count; i++)//ciclo for che si ripete per i giocatori della formazione del secondo giocatore
            {
                giocatores[indice_2].Formazione[i].PunteggioPartita = AssegnazionePunteggioCalciatore();//invoca la funzione AssegnazionePunteggioCalciatore, per ogni calciatore presente nell formazione, e gli assegna il valore di ritorno
            }
            giocatores[indice_1].CalcolaPunteggio();//invoca il metodo CalcolaPunteggio per il primo giocatore della partita
            giocatores[indice_2].CalcolaPunteggio();//invoca il metodo CalcolaPunteggio per il secondo giocatore della partita
            if (giocatores[indice_1].punteggio > giocatores[indice_2].punteggio)//se il punteggio
            {
                n = 2;
            }
            else if (giocatores[indice_1].punteggio < giocatores[indice_2].punteggio)
            {
                n = 1;
            }
            else
            {
                n = 0;
            }
            return n;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] calciatorifile;//array in cui viene assegnato il contenuto del file Calciatori.txt
            string[] nomigiocatori = new string[0];//array che contiene il nome dei giocatori 
            string filecalciatoripath = AppDomain.CurrentDomain.BaseDirectory + "Calciatori.txt";//stringa che contiene il percorso del file Calciatori.txt
            if (!File.Exists(filecalciatoripath))//controlla se il file Calciatori.txt non esiste
            {
                File.Create(filecalciatoripath);//crea il file Calciatori.txt specificando il percorso
                Console.WriteLine("Dato che non hai un file Calciatori.txt in cui sono contenuti i calciatori\n ne creo uno vuoto all'interno di esso inserisci i calciatori in questo modo\n ex. musso juan,portiere,atalanta,false");//scrive a schermo il contenuto all'interno delle virgole
            }
            else
            {
                calciatorifile = File.ReadAllLines(filecalciatoripath);//viene assegnato il contenuto del file Calciatori.txt nell'array calciatorifile
                List<Calciatore> calciatori = new List<Calciatore>();//creo la lista calciatori del tipo Calciatore
                Calciatore c = new Calciatore();//creo l'istanza c della classe Calciatore
                Giocatori g = new Giocatori();//creo l'istanza g della classe Giocatori
                Partita p = new Partita();//creo l'istanza p della classe Partita
                calciatori = c.GetCalciatori(calciatorifile);//invoca il metodo GetCalciatori della classe Calciatore che ,passando come parametro l'array calciatorifile, ritorna la lista che verrà assegnata a calciatori
                for (int i = 0; i < calciatori.Count; i++)//ciclo for che si ripete per tutti gli elementi della lista calciatori
                {
                    Console.WriteLine($"{calciatori[i].nome_e_cognome}");//scrive a schermo il valore dell'attributo nome_e_cognome del calciatore in posizione i
                }
                string risposta;//stringa 
                do//si ripete fino a quando il valore che viene ritornato dal metodo ControlloQuantità() corrisponde a false
                {
                    Console.WriteLine("Quanti giocatori vuoi inserire ? Il numero minimo e' 2 ed il massimo e' 10");//scrive a schermo i
                    risposta = Console.ReadLine();//viene assegnato nella variabile risposta ciò che l'utente ha scritto a schermo
                } while (!ControlloQuantità(risposta));

                int quantità = int.Parse(risposta);//variabile in cui viene assegnato risposta convertito in intero

                for (int i = 0; i < quantità; i++)//ciclo for che si ripete per il numero di giocatori inseriti dall'utente
                {
                    Console.WriteLine("inserisci il nome del " + (i + 1) + "° giocatore");//scrive a schermo il contenuto fra le virgolette
                    risposta = Console.ReadLine();//viene assegnato nella variabile risposta ciò che l'utente ha scritto a schermo
                    if (g.ControlloNomeInserito(risposta, nomigiocatori))//invoca la funzione ControlloNomeInserito, se ritorna true allora il nome non rispetta le condizioni 
                    {
                        i--;//diminuisce di 1 il valore di i per far ripete l'inserimento del nome
                    }
                    else
                    {
                        Array.Resize(ref nomigiocatori, nomigiocatori.Length + 1);//aumenta la grandezza dell'array nomigiocatori di 1 
                        nomigiocatori[i] = risposta;//assegno il valore di risposta in nomigiocatori in posizione i
                    }
                }
                List<Giocatori> giocatori = new List<Giocatori>();//creo la lista giocatori di tipo Giocatori
                for (int i = 0; i < nomigiocatori.Length; i++)//ciclo for che si ripete per tutti i giocatori
                {
                    giocatori.Add(new Giocatori());
                    giocatori[i].nome = nomigiocatori[i];
                    giocatori[i].crediti = 1100;
                }
                int[] prezzo = new int[giocatori.Count];
                int indice;
                int posizione = -1;
                do
                {
                    risposta = "";
                    Console.WriteLine("Inserisci il nome del calciatore da mettere all'asta (es mauro juan)");
                    risposta = Console.ReadLine();
                    posizione = c.ControlloCalciatoreAsta(ref calciatori, risposta);
                    while (posizione < 0)
                    {
                        Console.WriteLine("Il giocatore inserito non esiste oppure e' gia' stato assegnato\n Inserisci un'altro giocatore");
                        risposta = Console.ReadLine();
                        posizione = c.ControlloCalciatoreAsta(ref calciatori, risposta);
                    }
                    int i;
                    for (i = 0; i < giocatori.Count; i++)
                    {
                        do
                        {
                            Console.WriteLine($"Giocatore {giocatori[i].nome} inserisci il prezzo per l'asta per il calciatore");
                            prezzo[i] = giocatori[i].ControlloPrezzoInserito();
                        } while (prezzo[i] > 110);
                    }
                    indice = g.Asta(prezzo);
                    Console.WriteLine($"{giocatori[indice].nome} ha comprato il calciatore {calciatori[posizione].nome_e_cognome}");
                    giocatori[indice].AssegnazioneRosa(ref calciatori, posizione, prezzo[indice]);
                    Console.WriteLine($"{giocatori[indice].crediti}");
                    if (!g.ControlloRose(ref giocatori))
                    {
                        Console.WriteLine("Vuoi terminare l'asta?\n Se si allora scrivi yes \n in caso tu voglia continuare l'asta");
                        risposta = Console.ReadLine();
                    }
                } while (risposta != "Yes" && risposta != "Y" && risposta != "yes" && risposta != "y");
                Console.WriteLine("Ogni giocatore avra' una formazione composta da 11 giocatori\n la formazione per ciascun giocatore si basa sul modulo 4-3-3");
                //chiedo all'utente le formazioni di ogni giocatore
                int g_indice;
                for (g_indice = 0; g_indice < giocatori.Count; g_indice++)
                {
                    Console.WriteLine($"Rosa di {giocatori[g_indice].nome}");
                    for (int j = 0; j < giocatori[g_indice].Rosa.Count; j++)
                    {
                        Console.WriteLine($"{giocatori[g_indice].Rosa[j].nome_e_cognome}");

                    }
                    for (int i = 0; i < 12; i++)
                    {
                        if (i == 0)
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come portiere");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come portiere che appartenga alla rosa");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        else if (i <= 4)
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come difensore");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come difensore che non sia gia' stato inserito");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        else if (i <= 7)
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come centrocampista");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come centrocampista che non sia gia' stato inserito");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come attaccante");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come attaccante che non sia gia' stato inserito");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        giocatori[g_indice].AssegnazioneFormazione(ref giocatori[g_indice].Rosa, posizione);
                    }
                }
                do
                {
                    Console.WriteLine("");
                    int[] indici = p.GenerazionePartita(giocatori.Count);
                    int n1 = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine($"Giornata {j + 1} di 7\n");
                        for (int i = 0; i < giocatori.Count; i += 2)
                        {
                            n1++;
                            Console.WriteLine($"Girone {n1} di {indici.Length / 2}");
                            Console.WriteLine($"Si sfideranno {giocatori[indici[i]].nome} contro {giocatori[indici[i + 1]].nome}");

                            int valore = p.GestionePartita(ref giocatori, indici[i], indici[i + 1]);//invoca la funzione GestionePartita e il valore che ritorna viene assegnato ad risposte[5]
                            if (valore == 2)//nel caso in cui il valore ritornato corrisponde a 2
                            {
                                Console.WriteLine($"in questa partita ha vinto la squadra {giocatori[indici[i]].nome}");//viene scritto a schermo che la prima squadra delle due squadre della partita, ha vinto 
                            }
                            else if (valore == 1)//nel caso in cui il valore ritornato corrisponde a 1
                            {
                                Console.WriteLine($"In questa partita ha vinto la squadra {giocatori[indici[i + 1]].nome}");//viene scritto a schermo che la seconda squadra delle due squadre della partita, ha vinto 
                            }
                            else//nel caso in cui il valore ritornato corrisponde a 0
                            {
                                Console.WriteLine("Questa partita è terminata in pareggio");//viene scritto a schermo che la partita è terminata con il pareggio delle due squadre
                            }
                            giocatori[indici[i]].punteggio = 0;
                            giocatori[indici[i+1]].punteggio = 0;
                        }
                        n1 = 0;
                    }
                    p.ClassificaFinale(ref giocatori);
                    for (int d  = 0; d < giocatori.Count; d++)
                    {
                        Console.WriteLine($"In {d+1}° posizione abbiamo {giocatori[d].nome}");
                    }
                    Console.WriteLine("Scrivi yes per terminare il campionato");
                    risposta = Console.ReadLine();
                } while (risposta != "yes" && risposta != "y");
                p.ClassificaFinale(ref giocatori);
                for (int d = 0; d < giocatori.Count; d++)
                {
                    Console.WriteLine($"In {d + 1}° posizione abbiamo {giocatori[d].nome}");
                }
                Console.WriteLine("Grazie per aver giocato");
            }
        }
        private static bool ControlloQuantità(string risposta)//metodo che controlla il valore inserito da tastiera dall'utente e ritorna un valore booleano
        {
            try
            {
                int numero = int.Parse(risposta);//converte risposta in intero e il valore viene assegnato alla variabile numero
                if (numero < 2 || numero > 10 || numero %2 != 0)//controlla se numero non sia compreso tra 2 e 10 e se è dispari
                {
                    return false;//ritorna false
                }
                else//se non rispetta le condizioni
                {
                    return true;//ritorna true
                }
            }
            catch
            {
                return false;//ritorna false
            }
        }
    }
}
