package Module;

public enum Flags {
    StaticClass(1), // NotImplementation
    Delegate(2),
    AnonymousType(4), // NotImplementation
    Struct(8),
    Nested(16),
    Enum(32),
    Interface(64),
    PrivateMethods(128),
    StaticMethods(256),
    Property(512);

    private int code;

    Flags(int code){
        this.code = code;
    }
}
