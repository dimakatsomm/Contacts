namespace ContactManager.Data;

/// <summary>
/// Update Contact Request Data
/// </summary>
public interface IUpdateContactDto {
    /// <summary>
    /// Name of contact
    /// </summary>
    public string name { get; set; } 
    /// <summary>
    /// Email of contact
    /// </summary>
    public string email { get; set; } 
    /// <summary>
    /// Phone number of contact
    /// </summary>
    public string phone { get; set; } 
    /// <summary>
    /// Company name of contact
    /// </summary>
    public string company { get; set; } 
    /// <summary>
    /// Additional notes about the contact
    /// </summary>
    public string notes { get; set; } 

}

/// <summary>
/// Create Contact Request Data
/// </summary>
public interface ICreateContactDto: IUpdateContactDto {
    /// <summary>
    /// Flag if a contact is favourite or not
    /// </summary>
    public bool favourite { get; set; } 
}
