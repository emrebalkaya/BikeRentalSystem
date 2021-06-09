using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3
{
    class MaxHeap
    {
        public Durak[] arr;//altyapıda elemanları tutmak Durak tipinde dizi tanımlanır.
        private int sizeOfTree;
        // Create a constructor  
        public MaxHeap(int size)//Constructor
        {
            this.arr = new Durak[size + 1];//Size +1 olarak oluşturulur çüknü 0 indexi boş olacaktır.
            this.sizeOfTree = 0;
        }


        public int sizeoftree//Get set metodu.
        {
            get { return sizeOfTree; }
            set { sizeOfTree = value; }
        }
        public void InsertElementInHeap(Durak x)//Dizinin sonuna eleman eklenir ve Max Heap düzeni bozulmayacak şekilde dizi düzenlenir.
        {

            if (sizeoftree < 0)
            {
                Console.WriteLine("Tree is empty");
            }
            else
            {
                //Insertion of value inside the array happens at the last index of the  array
                arr[sizeoftree + 1] = x;//dizinin sonuna eklenir.
                sizeoftree++;//eleman sayısı arttırılır.
                HeapifyBottomToTop(sizeoftree);//dizi max heap düzenine göre düzenlenir.
            }
        }//end of method  
        public Durak[] Delete()//Normal bisiklet sayısı en fazla olan 3 durak heapten çekilir.
        {
            Durak[] deletedItems = new Durak[3];//çekilecek olan 3 durağı tutmak için dizi oluşturulur.
            for (int i = 0; i < 3; i++)
            {
                Durak root = arr[1];//Çekilecek eleman geçici değişkende tutulur.
                arr[1] = arr[sizeoftree--];//dizinin ilk elemanı çekilir(en büyük olacağından dolayı) ve eleman sayısı düşürülür.
                HeapifyTopToBottom(1);//dizi max heap düzenine göre düzenlenir.
                deletedItems[i] = root;//çekilen eleman deleted dizisinde tutulur.
            }
            return deletedItems;
        }
        public void HeapifyTopToBottom(int index)//Heap' i yukarıdan aşağı dolaşarak max heap düzenine sokan metod.
        {
            int largest;//en büyük değişkeni tutmak için oluşturulur.
            Durak top = arr[index];//dizinin girilen indexteki elemanı geçici tutulur (en üstteki eleman).
            while (index < sizeoftree / 2)//Bulunduğumuz düğümün en azından 1 çocuğu varsa
            {
                int left = 2 * index;//bulunduğumuz düğümün sol ve sağ çocukları belirlenir.
                int right = left + 1;
                if (right < sizeoftree && arr[left].NormalBis < arr[right].NormalBis)//eğer sağ çocuk varsa ve sağ çocuk sol çocuktan büyükse
                    largest = right;//largest sağ çocuk olur
                else
                    largest = left;//değilse largest left çocuk olur.
                if (top.NormalBis >= arr[largest].NormalBis)//eğer Heap'in baş elemanı largesttan büyük veya eşitse sıralamaya gerek yoktur. 
                    break;
                arr[index] = arr[largest];//Değilse largest o index'e atanır.
                index = largest;//index largest olarak belirlenir ve döngü devam eder.
            }
            arr[index] = top;//döngü bittikten sonra top değeri index nerede kaldıysa oraya konulur.
        }
        public void HeapifyBottomToTop(int index)//Heap' i aşağıdan yukarıya dolaşarak max heap düzenine göre sıralar.
        {
            int parent = index / 2;//girilen indexin parenti belirlenir.
            if (index <= 1)//eğer index 1 den küçükse heap'in başındayız demektir artık sıralamaya gerek yoktur.
            {
                return;
            } 
            if (arr[index].NormalBis > arr[parent].NormalBis)//eğer girilen indexteki değer parent'ından büyükse 
            {
                Durak tmp = arr[index];//Swap işlemi gerçekleştirilir.
                arr[index] = arr[parent];
                arr[parent] = tmp;
            }
            HeapifyBottomToTop(parent);//Sıralamaya parent düğümden devam edilir.
        }

        public void levelOrder()//Heap içindeki bilgileri yazdırma metodu.
        { 
            for (int i = 1; i <= sizeoftree; i++)
            {
                Console.WriteLine(arr[i] + "\n--------------------------------");
            }
            Console.WriteLine("\n");
        } 

    }
}
