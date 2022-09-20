using MyCalculator.Interfaces;
using MyCalculator.Views;

namespace Tests;

public class CalculatorViewTests : IDisposable
{
    private ICalculatorView _calculatorView;

    private TestStream _stream = new();

    private const string FirstArgument = "123";
    private const string SecondArgument = "321";

    public CalculatorViewTests()
    {
        _calculatorView = new CalculatorView(_stream);
    }

    # region InitializeView

    [Fact]
    public void InitializeView_ShouldThrowArgumentException_WhenStreamIsNotWritable()
    {
        _stream = new TestStream(canWrite: false);

        var act = () => new CalculatorView(_stream);

        act.Should().Throw<ArgumentException>()
            .WithMessage("Stream was not writable.");
    }

    [Fact]
    public void InitializeView_ShouldThrowArgumentException_WhenStreamIsNotReadable()
    {
        _stream = new TestStream(canRead: false);

        var act = () => new CalculatorView(_stream);
        
        act.Should().Throw<ArgumentException>()
            .WithMessage("Stream was not readable.");
    }

    [Fact]
    public void InitializeView_ShouldNotThrowException_WhenStreamIsWritableAndReadable()
    {
        _stream = new TestStream(true, true);

        var act = () => new CalculatorView(_stream);

        act.Should().NotThrow<Exception>();
    }

    # endregion
    
    # region PrintResult

    [Fact]
    public void PrintResult_ShouldWriteResultToStream_WhenStreamIsOpen()
    {
        const int result = 12345;

        _calculatorView.PrintResult(result);

        var resultMessage = _stream.ReadLastLine()!;
        resultMessage.Should().Contain(result.ToString());
    }
    
    [Fact]
    public void PrintResult_ShouldThrowObjectDisposedException_WhenStreamWasClosedAfterInitializingView()
    {
        _stream = new TestStream();
        _calculatorView = new CalculatorView(_stream);
        _stream.Close();
        const int result = 12345;

        var act = () => _calculatorView.PrintResult(result);

        act.Should().Throw<ObjectDisposedException>();
    }
    
    # endregion
    
    # region DisplayError

    [Fact]
    public void DisplayError_ShouldWriteErrorMessageToStream_WhenStreamIsOpen()
    {
        const string errorMessage = "My test message";

        _calculatorView.DisplayError(errorMessage);

        var resultMessage = _stream.ReadLastLine()!;
        resultMessage.Should().Contain(errorMessage);
    }
    
    [Fact]
    public void DisplayError_ShouldThrowObjectDisposedException_WhenStreamWasClosedAfterInitializingView()
    {
        _stream = new TestStream();
        _calculatorView = new CalculatorView(_stream);
        _stream.Close();
        const string errorMessage = "My test message";

        var act = () => _calculatorView.DisplayError(errorMessage);

        act.Should().Throw<ObjectDisposedException>();
    }
    
    # endregion

    # region GetArguments

    [Fact]
    public void GetFirstArgumentAsString_ShouldReturnFirstArgument_WhenStreamDoesContainArguments()
    {
        WriteArguments(_stream);
        _stream.ResetPosition();

        var result = _calculatorView.GetFirstArgumentAsString();

        result.Should().Be(FirstArgument);
    }

    [Fact]
    public void GetFirstArgumentAsString_ShouldThrowInvalidOperationException_WhenStreamIsEmpty()
    {
        _stream.SetLength(0);

        var act = () => _calculatorView.GetFirstArgumentAsString();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GetSecondArgumentAsString_ShouldReturnSecondArgument_WhenStreamDoesContainArguments()
    {
        WriteArguments(_stream);
        _stream.ResetPosition();

        var result = _calculatorView.GetSecondArgumentAsString();

        result.Should().Be(SecondArgument);
    }

    [Fact]
    public void GetSecondArgumentAsString_ShouldThrowInvalidOperationException_WhenStreamDoesContainOnlyFirstArgument()
    {
        _stream.WriteLine(FirstArgument);
        _stream.ResetPosition();

        var act = () => _calculatorView.GetSecondArgumentAsString();

        act.Should().Throw<InvalidOperationException>();
    }

    # endregion

    public void Dispose()
    {
        _stream.Close();
        GC.SuppressFinalize(this);
    }

    private static void WriteArguments(TestStream stream)
    {
        stream.WriteLine(FirstArgument);
        stream.WriteLine(SecondArgument);
    }
}