
public static class Utils{

    private static System.Random rand = new System.Random();

    public static void Shuffle<T>(this T[] src){

        int n = src.Length;
        while(n>0){
            n--;
            int k = rand.Next(n+1)%src.Length;
            T val1 = src[n];
            T val2 = src[k];
            src[n] = val2;
            src[k] = val1;
        }


    }

}