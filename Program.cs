using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3
{    
    class TreeNode
    {
        public Durak data;
        public TreeNode leftChild;
        public TreeNode rightChild;
        public void displayNode()
        {
            Console.WriteLine(data);
        }
    }
    class Müşteri
    {
        private int ID;
        private double kiralamaSaati;
        public Müşteri(int ID, double kiralamaSaati)
        {
            this.ID = ID;
            this.kiralamaSaati = kiralamaSaati;
        }
        public int id
        { 
            get { return ID; }
        }
        public double kiralama
        {
            get { return kiralamaSaati; }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            String[] duraklar = { "İnciraltı,28,2,10", "Sahilevleri,8,1,11", "Doğal Yaşam Parkı,17,1,15", "Bostanlı İskele,7,0,7","Mavi Bahçe,6,1,12","Pasaport İskele,10,0,5",
            "Bornova Metro,6,0,6","Karşıyaka İskele,11,3,9","Üçkuyular İskele,19,1,10","Karantina,7,0,8"};
            Tree bt = new Tree();
            int müşterisayısı = 0;
            Random rnd = new Random();
            for (int i = 0; i < duraklar.Length; i++)
            {
                String[] durakSplit = duraklar[i].Split(',');//Durak bilgileri duraklar dizisinden çekilir.
                Durak durak = new Durak(durakSplit[0], int.Parse(durakSplit[1]), int.Parse(durakSplit[2]), int.Parse(durakSplit[3]));//Çekilen bilgilerle durak nesnesi oluşturulur.
                bt.insert(durak);//Duraklar ağacın düğümlerine eklenir.
                List<Müşteri> müşteriList = new List<Müşteri>();//Her durak için müşterileri tutacak generic list oluşturulur.
                int randomnumber = rnd.Next(1, 11);//İlgili durağa kaç tane müşteri atılacağı random belirlenir.
                TreeNode currentNode = bt.FindNode(durak);//İlgili durağın ağaçtaki düğümü bulunur.
                if (currentNode.data.durakBosMu(bt.getRoot()) == false)//Eğer durakta bisiklet varsa kiralama işlemine geçilir.
                {
                    for (int j = 0; j < randomnumber; j++)
                    {
                        int randomID = rnd.Next(1, 21);//Müşteri numarası(ID) random belirlenir.
                        double randomsaat = Convert.ToDouble((Convert.ToString(rnd.Next(0, 25)) + "," + Convert.ToString(rnd.Next(0, 60))));//Kiralama saati random belirlenir.
                        Müşteri müşteri = new Müşteri(randomID, randomsaat);//Müşteri nesnesi oluşturulur.
                        if (currentNode.data.NormalBis > 0)//Eğer normal bisiklet varsa müşteri normal bisiklet kiralar.
                        {
                            currentNode.data.BosPark++;//Boş park sayısı arttırılır.    
                            currentNode.data.NormalBis--;//Normal bisiklet sayısı azaltılır.
                        }
                        else if (currentNode.data.TandemBis > 0)//Normal bisiklet için uygulanan işlemlerin aynısı uygulanır.
                        {
                            currentNode.data.BosPark++;
                            currentNode.data.TandemBis--;
                        }
                        else
                            break;
                        müşteriList.Add(müşteri);//Müşteri listeye eklenir ve müşteri sayısı arttırılır.
                        müşterisayısı++;
                    }
                    currentNode.data.DurakList(currentNode, müşteriList);//Oluşturulan liste durak altında saklanır.
                }
            }
            bt.preOrder(bt.getRoot());//Ağaç pre order yazdırılır.
            Console.WriteLine("Ağacın derinliği: " + bt.FindDepth(bt.getRoot()));//Ağacın derinliği fonksiyon yardımıyla bulunur ve yazdırılır.
            Console.WriteLine("Toplam yapılan kiralama işlemi(müşteri sayısı): " + müşterisayısı);//Toplam kiralama işlemi yazdırılır.
            Console.WriteLine("Kiralama geçmişi sorgusu yapılacak müşteri numarasını(ID) giriniz:");
            int inputıd = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Müşterinin kiralama geçmişi:");
            bt.SearchElement_Rec(bt.getRoot(), inputıd);//ID si girilen müşterinin kiralama geçmişi ağaçta aranarak yazdırılır.
            Console.WriteLine("Kiralama yapacak müşterinin müşteri numarasını(ID) giriniz:");
            int kiralamaıd = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Kiralama yapılacak durağın adını giriniz: ");
            string kiralamadurak = Console.ReadLine();
            bt.Kiralama(kiralamadurak, kiralamaıd);// Kiralama yapacak kişinin ID numarasına ve durak adına göre kiralama işlemi yapılır gerekli durak güncellemeleri yapılır.
            //--------HASHTABLE--------------
            Console.WriteLine("******************HASHTABLE*****************");
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < duraklar.Length; i++)
            {
                String[] durakSplitHT = duraklar[i].Split(',');
                Durak durakHT = new Durak(durakSplitHT[0], int.Parse(durakSplitHT[1]), int.Parse(durakSplitHT[2]), int.Parse(durakSplitHT[3]));//Duraklar dizisinden çekilen bilgilerle durak nesnesi oluşturulur.
                hashtable.Add(durakHT.DurakAdı, durakHT);//Oluşturulan durak, durak adına göre hashtable' a atılır.
            }
            foreach (object Anahtar in hashtable.Keys)  //Hashtable'ın yazdırılması
                Console.WriteLine(hashtable[Anahtar]+"\n-------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Boş park sayısı 5'ten fazla olan duraklara normal bisiklet ekleniyor...");
            for (int i = 0; i < duraklar.Length; i++)
            {
                String[] durakSplitHT = duraklar[i].Split(',');
                int bosParkParse= int.Parse(durakSplitHT[1]);
                int normalBisikletParse = int.Parse(durakSplitHT[3]);
                if (bosParkParse>5)//Boş Park sayısı 5'ten büyük olan duraklara normal bisiklet yüklenir ve durak bilgileri güncellenir.
                {
                    bosParkParse = bosParkParse - 5;
                    normalBisikletParse = normalBisikletParse + 5;
                }
                Durak durakHT = new Durak(durakSplitHT[0], bosParkParse, int.Parse(durakSplitHT[2]), normalBisikletParse);
                hashtable[durakSplitHT[0]] = durakHT;
            }
            foreach (object Anahtar in hashtable.Keys)//Hashtable yeni hali yazdırılır.
                Console.WriteLine(hashtable[Anahtar] + "\n-------------------------------");
            Console.WriteLine("");
            Console.WriteLine("****************MAX HEAP**************");
            MaxHeap heap = new MaxHeap(10);//Max Heap veri yapısı oluşturulur.
            for (int i=0; i < duraklar.Length; i++)
            {
                String[] durakSplit = duraklar[i].Split(',');
                Durak durak = new Durak(durakSplit[0], int.Parse(durakSplit[1]), int.Parse(durakSplit[2]), int.Parse(durakSplit[3]));
                heap.InsertElementInHeap(durak);//Duraklar dizisinden çekilen bilgilerle durak nesnesi oluşturularak heap' e Max Heap olacak şekilde atılır.
            }
            heap.levelOrder();//Heap yazdırılır.
            Durak[] deleted = heap.Delete();//Normal bisiklet sayısı en fazla olan 3 durak heap'ten çekilir ve yazdırılır.
            Console.WriteLine("Normal bisiklet sayısı en fazla olan 3 durak ve bilgileri: ");
            for (int i = 0; i < deleted.Length; i++)
            {
                Console.WriteLine(deleted[i]+ "\n--------------------------------");                
            }
            Console.ReadKey();            
        }
    }
}   
