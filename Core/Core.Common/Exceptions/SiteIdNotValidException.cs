using System;
using ConventionsAide.Core.Common.Properties;

namespace ConventionsAide.Core.Common.Exceptions;

public class SiteIdNotValidException:Exception
{
    public SiteIdNotValidException() : base(Resources.ERR_SITE_ID_NOT_VALID)
    {

    }

    public SiteIdNotValidException(Exception ex) : base(Resources.ERR_SITE_ID_NOT_VALID, ex)
    {

    }
}