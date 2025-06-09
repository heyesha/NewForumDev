namespace NewForumDev.Domain.Reports;

public enum ReportStatus
{
    /// <summary>
    /// Статут открыт
    /// </summary>
    OPEN,

    /// <summary>
    /// Статус в работе
    /// </summary>
    IN_PROGRESS,
    
    /// <summary>
    /// Статус решён
    /// </summary>
    RESOLVED,
    
    /// <summary>
    /// Статус закрыт
    /// </summary>
    DESMISSED,
}