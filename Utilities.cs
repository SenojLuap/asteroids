using System;

namespace Asteroids {

    public static class Utilities {

        public static string ExceptionToS(Exception ex) => String.Format("[{0}|{1}] {2}\n{3}", ex.GetType().Name, ex.Source, ex.Message, ex.StackTrace);
    }
}