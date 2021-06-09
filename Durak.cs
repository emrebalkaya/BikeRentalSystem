using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3
{
    class Durak
    {
        private String durakAdı;
        private int bosPark, tandemBis, normalBis;
        public List<Müşteri> müşteriList;//Müşterileri tutacak generic list.
        public Durak(String durakAdı, int bosPark, int tandemBis, int normalBis)//Constructor.
        {
            this.durakAdı = durakAdı;
            this.bosPark = bosPark;
            this.tandemBis = tandemBis;
            this.normalBis = normalBis;
        }
        public void DurakList(TreeNode currentNode, List<Müşteri> x)//Oluşturulan müşteri listesini durak nesnesi içinde tutan metod.
        {
            currentNode.data.müşteriList = new List<Müşteri>();
            foreach (Müşteri i in x)
            {
                currentNode.data.müşteriList.Add(i);
            }

        }
        //get , set ve toString metodları.
        public String DurakAdı
        {
            get { return durakAdı; }

        }
        public int BosPark
        {
            get { return bosPark; }
            set { bosPark = value; }
        }
        public int TandemBis
        {
            get { return tandemBis; }
            set { tandemBis = value; }
        }
        public int NormalBis
        {
            get { return normalBis; }
            set { normalBis = value; }
        }
        public override string ToString()
        {
            return "Durak Adı: " + durakAdı + "\nBoş Park Sayısı: " + bosPark + "\nTandem Bisiklet Sayısı: " + tandemBis + "\nNormal Bisiklet Sayısı: " + normalBis;
        }
        public bool durakBosMu(TreeNode x)//Durakta bisikletin olup olmadığını kontrol eden metod.
        {
            if (x.data.TandemBis + x.data.NormalBis == 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
