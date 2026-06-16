public class LowPressureGVS : Alarm
{
    //��������� ������� ���� � ���� ������ ����
    //��������� ����������������� ������� ��������
    //���������(��� ������������� ���������) �������������� ���
    //��������� �� ������, ������� ���������
    //��������� ��������� ������� ��������
    //private string _nameAlarm = "������ �������� � GVS";

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void UpdateInteractionSelect(InteractionInformation interactionSelect)
    {
        base.UpdateInteractionSelect(interactionSelect);
    }

    protected override void CorrectObject()
    {
        base.CorrectObject();
    }

    protected override void ButtonClick(int nomerButton)
    {
        base.ButtonClick(nomerButton);
    }

    protected override void InteractionInfoTextColorUpdate(bool value)
    {
        base.InteractionInfoTextColorUpdate(value);
    }

    protected override void TaskTextColorUpdate(int subsequenceInteraction, bool interactionSelect)
    {
        base.TaskTextColorUpdate(subsequenceInteraction, interactionSelect);
    }

    protected override void CehekingCorrectButton(int nomerButton)
    {
        base.CehekingCorrectButton(nomerButton);
    }

    protected override void ViktoryChek()
    {
        base.ViktoryChek();
    }

    protected override void ButtonActionClick()
    {
        base.ButtonActionClick();
    }
}
