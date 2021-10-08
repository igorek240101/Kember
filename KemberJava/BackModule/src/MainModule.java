import java.util.ArrayList;
import java.io.*;

public class MainModule {

    static ArrayList<IMetric> metrics;

    static ArrayList<String> saves = new ArrayList<>();

    public static String user;

    static {
        File file = new File("Module");
        String[] s = file.list();
        for(String value : s) {
            Class _class;
            try {
                _class = Class.forName(value);
                Class[] intefaces =  _class.getInterfaces();
                for(Class _interface : intefaces){
                    if(_interface.equals(Class.forName("IMetric"))){
                        try {
                            metrics.add((IMetric) _class.newInstance());
                        }
                        catch (Exception e){

                        }
                        break;
                    }
                }
            } catch (ClassNotFoundException e) {
                e.printStackTrace();
            }
        }
    }

    public static ArrayList<Cortege<Object, Class<?>>> invoke(Class<?>[] assembly, Object args, String metric) {
        ArrayList<Cortege<Object, Class<?>>> results = new ArrayList<>();
        for (IMetric iMetric : metrics) {
            if (iMetric.getClass().getName().equals(metric)) {
                for (int j = 0; j < results.size(); j++) {
                    results.add(new Cortege<>(iMetric.runMetric(assembly[j], args), assembly[j]));
                }
                saves.add("");
                return results;
            }
        }
        return null;
    }

    public static void save(String key) {
        if (hashValidate(key)) //throw new Exception();
        //String name = user.toString();
        while (saves.size() > 0)
        {
            String path = "Data\\" + user.toString() + saves.get(0).toString() + saves.get(0).toString() + ".kbr";
            //AppDbContext.db.Logs.Add(new Log() { Owner = User, Metric = saves[0].Item1, TimeMark = saves[0].Item2, PathToFile = path});
            //Encrypt(key, saves[0].Item3, path);
        }
    }

    public static boolean hashValidate(String key) {
        //if (user == null) throw new Exception();
        //SHA512Managed sha = new SHA512Managed();
        //string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(key)));
        //return hash == User.SecurityKey;
        return true;
    }

    public static Cortege<Class<?>,Object> load(String key, String log) {
        if (hashValidate(key)) //throw new Exception();
        for(var value : metrics)
        {
            if(value.getClass().getName().equals(log))
            {
                return value.read(decrypt(key, log));
            }
        }
        return null;
        //throw new Exception();
    }

    private static void encrypt(String skey, String text, String path) {
        //Aes aes = Aes.Create();
        //byte[] key = Encoding.UTF8.GetBytes(skey);
        //aes.Key = key;
        //byte[] iv = aes.IV;
        //FileStream file = new FileStream(path, FileMode.Create);
        //file.Write(iv, 0, iv.Length);

        //using (CryptoStream cryptoStream = new(file, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
           // using (StreamWriter encryptWriter = new(cryptoStream))
            {
             //   encryptWriter.WriteLine(text);
            }
        }
    }

    private static String decrypt(String skey, String path) {
        //FileStream fileStream = new(path, FileMode.Open);
        //Aes aes = Aes.Create();
        //byte[] iv = new byte[aes.IV.Length];
        //int numBytesToRead = aes.IV.Length;
        //int numBytesRead = 0;
        //while (numBytesToRead > 0)
        {
            //int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
            //if (n == 0) break;

            //numBytesRead += n;
            //numBytesToRead -= n;
        }
        //byte[] key = Encoding.UTF8.GetBytes(skey);
        //CryptoStream cryptoStream = new(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
        //StreamReader decryptReader = new(cryptoStream);
        //return decryptReader.ReadToEnd();
        return "";
    }


    public static boolean login(String name)
    {
        //User user = AppDbContext.db.Users.FirstOrDefault(t => t.Name == name);
        if (user != null)
        {
            //User = user;
            return true;
        }
        else return false;
    }


    public static void registration(String name, String key)
    {
        //SHA512Managed sha = new SHA512Managed();
        //String hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(key)));
        //User = new User() { Name = name, SecurityKey = hash };
        //AppDbContext.db.Users.Add(User);
        //AppDbContext.db.SaveChanges();
    }
}


