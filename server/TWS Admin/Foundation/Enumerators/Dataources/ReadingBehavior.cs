namespace Foundation;

/// <summary>
///     Indicates different kinds of behaviors for the 
///     datasource repository Read operation
///     
///     For (<see cref="First"/> and <see cref="Last"/>)
///     Consider that the Collection where the operation will be performed, 
///     won't be ordered or modified from the retrieved one, so 
///     they will get the items directly from the original order
///     of the Collection fetched.
/// </summary>
public enum ReadingBehavior {
    /// <summary>
    ///     Will return only the first item that matches.
    /// </summary>
    First,
    /// <summary>
    ///     Will return only the last item that matches.
    /// </summary>
    Last,
    /// <summary>
    ///     Will return all the items that match.
    /// </summary>
    All,
}
