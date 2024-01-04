namespace Foundation.Enumerators.Exceptions;
public enum ConnectionLoadFailureReasons {
    CallerPathEmpty,
    ParentProjectPathEmpty,
    ConnectionDirectoryUnfound,
    ConnectionPropertiesUnfound,
    WrongPropertiesFileFormat,
    IOCriticalException,
}
