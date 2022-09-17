namespace Calculator.Interfaces;

public interface ICalculatorPresenter
{
    /**
     * Called by the form when the user has clicked the '+' button
     */
    void OnPlusClicked();

    /**
     * Called by the form when the user has clicked on the '-' button
     */
    void OnMinusClicked();

    /**
     * Called by the form when the user clicks on the '/' button
     */
    void OnDivideClicked();

    /**
     * Called by the form when the user has clicked on the '*' button
     */
    void OnMultiplyClicked();
}