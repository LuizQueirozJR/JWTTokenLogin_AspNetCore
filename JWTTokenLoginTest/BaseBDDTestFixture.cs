using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

[Specification]
public abstract class BaseBDDTestFixture
{
    protected Exception CaughtException;

    public abstract void Initialize();
    public abstract void Given();
    public abstract void When();

    [TestInitialize]
    public void Setup()
    {
        try { Initialize(); } catch { throw; }
        try { Given(); } catch { throw; }
        try { When(); }
        catch (Exception exception)
        {
            CaughtException = exception;
        }
    }

    protected bool OcorreuExcecao(string exceptionMessage, Exception exception)
    {
        if (exception != null && exception.Message == exceptionMessage)
        {
            return true;
        }

        return OcorreuExcecao(exceptionMessage, exception.InnerException);
    }
}

public class ThenAttribute : TestMethodAttribute { }

public class SpecificationAttribute : TestClassAttribute { }