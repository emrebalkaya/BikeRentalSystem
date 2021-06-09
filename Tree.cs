using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3
{
    class Tree
    {
        private TreeNode root;
        private int Bstsize;
        public Tree()
        {
            root = null;
            Bstsize = 1;
        }
        public TreeNode getRoot()
        { return root; }
        // Ağacın preOrder Dolasılması
        public void preOrder(TreeNode localRoot)//Ağaç pre order şekilde dolaşılır ve ilgili bilgiler yazdırılır.
        {
            if (localRoot != null)
            {
                localRoot.displayNode();
                if (localRoot.data.müşteriList != null)
                {
                    Console.WriteLine("Bu durakta " + localRoot.data.müşteriList.Count + " müşteri kiralama yapmıştır.");//İlgili durakta kaç tane müşteri kiralama işlemi yaptıysa yazdırılır.
                    Console.WriteLine("Yapılan kiralama işlemleri:");
                    Console.WriteLine("-----------------------------------");
                    foreach (Müşteri x in localRoot.data.müşteriList)
                    {
                        string kiralamastr = Convert.ToString(x.kiralama);//Kiralama saatinin tam saat görünümü kazanması için gerekli işlemler yapılır.
                        string[] kiralamasplit = kiralamastr.Split(',');
                        if (kiralamasplit.Length == 2)
                        {
                            if (kiralamasplit[0].Length == 1)
                                kiralamasplit[0] = "0" + kiralamasplit[0];
                            if (kiralamasplit[1].Length == 1)
                                kiralamasplit[1] = "0" + kiralamasplit[1];
                            kiralamastr = kiralamasplit[0] + ":" + kiralamasplit[1];
                        }
                        else
                        {
                            if (kiralamasplit[0].Length == 1)
                                kiralamasplit[0] = "0" + kiralamasplit[0];
                            kiralamastr = kiralamasplit[0] + ":" + "00";
                        }
                        Console.WriteLine("Müşteri ID: " + x.id + "| Kiralama Saati: " + kiralamastr);//müşterilerin bilgileri yazdırılr.
                    }
                }
                Console.WriteLine();
                preOrder(localRoot.leftChild);//Fonskiyon recursive olarak bütün ağacı dolaşır ve bütün ağaçtaki bilgileri yazdırır.
                preOrder(localRoot.rightChild);
            }
        }
        public bool varmı = false;
        public int count = 0;

        public void SearchElement_Rec(TreeNode currentRoot, int ID)//ID numarası girilen müşterinin kiralama sorgusu.
        {

            if (currentRoot != null)
            {   

                for (int i = 0; i < currentRoot.data.müşteriList.Count; i++)
                {
                    if (ID == currentRoot.data.müşteriList[i].id)//Müşteri ID si durakta varsa eğer kiralama geçmişi yazdırılır.
                    {
                        string kiralamastr = Convert.ToString(currentRoot.data.müşteriList[i].kiralama);//Kiralama saati düzenlenir.
                        string[] kiralamasplit = kiralamastr.Split(',');
                        if (kiralamasplit.Length == 2)
                        {
                            if (kiralamasplit[0].Length == 1)
                                kiralamasplit[0] = "0" + kiralamasplit[0];
                            if (kiralamasplit[1].Length == 1)
                                kiralamasplit[1] = "0" + kiralamasplit[1];
                            kiralamastr = kiralamasplit[0] + ":" + kiralamasplit[1];
                        }
                        else
                        {
                            if (kiralamasplit[0].Length == 1)
                                kiralamasplit[0] = "0" + kiralamasplit[0];
                            kiralamastr = kiralamasplit[0] + ":" + "00";
                        }
                        varmı = true;
                        Console.WriteLine("Müşteri " + currentRoot.data.DurakAdı + " durağında " + kiralamastr + " saatinde kiralama işlemi gerçekleştirmiştir.");//Kiralama geçmişi yazdırılır.
                    }
                }
                SearchElement_Rec(currentRoot.leftChild, ID);//Fonksiyon recursive şekilde bütün ağacı arar.
                SearchElement_Rec(currentRoot.rightChild, ID);

            }

            count++;
            if (!varmı && count == Bstsize * 2 + 1)//Eğer bütün ağaç dolaşılmışsa ve girilen ID'ye sahip bir kiralama geçmişi bulunamadıysa bulunamadı yazdırılır.
                Console.WriteLine("Müşteri kiralama işlemi gerçekleştirmemiştir.");
        }
        public void Kiralama(string kiralamaDurak, int kiralamaID)//ID'sini ve kiralama yapmak istediği durağı giren müşterinin kiralama işlemi gerçekleştirilir.
        {
            Random rand = new Random();
            TreeNode current = root;
            if (current != null)
            {
                while (string.Compare(current.data.DurakAdı, kiralamaDurak) != 0)//İlgili durak adı bulunana kadar döngü devam eder.
                {
                    if (string.Compare(current.data.DurakAdı, kiralamaDurak) > 0)
                    {
                        current = current.leftChild;
                    }

                    //else go to right tree
                    else
                    {
                        current = current.rightChild;
                    }
                    //not found
                    if (current == null)
                    {
                        Console.WriteLine("Durak bulunamadı!");
                    }
                }
                if ((string.Compare(current.data.DurakAdı, kiralamaDurak) == 0))//Girilen durak bulununca kiralama işlemine geçilir.
                {
                    if (current.data.NormalBis>0)//Durakta normal bisiklet var mı diye kontrol edilir. Eğer varsa kiralama işlemi gerçekleştirilir ve durak bilgileri güncellenir.
                    {
                        double kiralamasaati = Convert.ToDouble((Convert.ToString(rand.Next(0, 25)) + "," + Convert.ToString(rand.Next(0, 60))));
                        Müşteri müşteri = new Müşteri(kiralamaID, kiralamasaati);
                        current.data.müşteriList.Add(müşteri);
                        current.data.NormalBis--;
                        current.data.BosPark++;
                        Console.WriteLine("Kiralama yapılan durak: \n" + current.data + "\n");
                    }
                    else//Eğer durakta normal bisiklet kalmamışsa başka bir durak adı girilmesi veya çıkış yapılması istenir.
                    {
                        Console.WriteLine("Bu durakta bisiklet kalmamıştır.");
                        Console.WriteLine("Başka duraktan kiralama yapmak istiyorsanız yeni durak adı giriniz(çıkış için h/H giriniz):");
                        string cevap = Console.ReadLine();
                        if (string.Compare(cevap, "h") == 0 || string.Compare(cevap, "h") == 0)
                            return;
                        else
                        {
                            Kiralama(cevap, kiralamaID);
                        }
                    }
                }
            }
        }
        // Agacın inOrder Dolasılması
        public void inOrder(TreeNode localRoot)//Ağacın inorder şekilde yazdırılması.
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.displayNode();
                inOrder(localRoot.rightChild);
            }
        }
        // Agacın postOrder Dolasılması
        public void postOrder(TreeNode localRoot)//Ağacın post order şekilde yazdırılması.
        {
            if (localRoot != null)
            {
                postOrder(localRoot.leftChild);
                postOrder(localRoot.rightChild);
                localRoot.displayNode();
            }
        }
        public int FindDepth(TreeNode node)//Ağacın derinliğini bulan method
        {
            if (node == null)//Eğer ağacın başı null ise ağaç boştur ve 0 döndürülür.
                return 0;
            else
            {
                //Sağ ve sol alt ağacın derinliği revursive olarak hesaplanır.
                int leftDepth = FindDepth(node.leftChild);
                int rightDepth = FindDepth(node.rightChild);
                return Math.Max(rightDepth, leftDepth) + 1;//Derinliği büyük olan döndürülür.
            }
        }
        public TreeNode FindNode(Durak x)//Girilen durak adının ağaçtaki konumunu bulan metod.
        {
            TreeNode current = root;//Ağacın başından başlanarak durak adına göre bütün ağaç gezilir.
            while (string.Compare(current.data.DurakAdı, x.DurakAdı) != 0)
            {
                if (current != null)
                {
                    if (string.Compare(current.data.DurakAdı, x.DurakAdı) > 0)
                    {
                        current = current.leftChild;
                    }  //else go to right tree
                    else
                    {
                        current = current.rightChild;
                    }
                    //not found
                    if (current == null)
                    {
                        Console.WriteLine("Durak bulunamadı!");
                    }
                }
            }
            return current;//Durağın ait olduğu düğüm döndürülür.
        }
        public void insert(Durak newdata)//Max order şekilde girilen durak nesneleri durak adlarına göre ağaca eklenir.
        {
            TreeNode newNode = new TreeNode();
            newNode.data = newdata;//Girilen durak yeni nesne oluşturularak içine atılır.
            if (root == null)//Eğer ağaç boşsa ilk eleman olarak ağaca eklenir.
                root = newNode;
            else//Ağaç boş değilse diğer düğümler kontrol edilir.
            {
                TreeNode current = root;
                TreeNode parent;
                while (true)
                {
                    parent = current;
                    if (string.Compare(newdata.DurakAdı, current.data.DurakAdı) == -1)//Eğer girilen durak adı şu anki durak adından küçükse sol çocuğa gidilir.
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;//eğer sol çocuk yok ise sol çocuk olarak yeni durak eklenir.
                            Bstsize++;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightChild;//Değerler eşitse veya girilen girilen büyükse sağ çocuğa gidilir
                        if (current == null)
                        {
                            parent.rightChild = newNode;//Sağ çocuk yoksa sağ çocuk olarak ağaca eklenir.
                            Bstsize++;
                            return;
                        }
                    }
                }
            } 
        } 
    }
}
