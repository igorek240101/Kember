package BackModule;

public interface IMetric {

    public Object runMetric(Class<?> _class,Object args);

    public Cortege<Class<?>, Object> read(String input);

    public String write(Cortege<Class<?>, Object> output);
}
