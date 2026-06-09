namespace IgApplication.Services;

public enum NotificationType { Success, Error, Warning, Info }

public record ToastMessage(string Text, NotificationType Type = NotificationType.Info);

public class NotificationService
{
    public event Action<ToastMessage>? OnShow;

    public void ShowSuccess(string message) => OnShow?.Invoke(new(message, NotificationType.Success));
    public void ShowError(string message)   => OnShow?.Invoke(new(message, NotificationType.Error));
    public void ShowWarning(string message) => OnShow?.Invoke(new(message, NotificationType.Warning));
    public void ShowInfo(string message)    => OnShow?.Invoke(new(message, NotificationType.Info));
}
