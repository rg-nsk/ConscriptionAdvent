using System;

namespace ConscriptionAdvent.Data.Firebird.Dto
{
    public partial class SPEC : IEquatable<SPEC>
    {
        public override bool Equals(object obj)
        {
            var source = obj as SPEC;
            if (source == null) return false;

            return Equals(source);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^
                   NAME.GetHashCode();
        }

        public bool Equals(SPEC other)
        {
            return ID == other.ID &&
                   NAME == other.NAME;
        }
    }
}
