using UnityEngine;

[System.Serializable] //Pour appeler ce script d'un autre script
public class Dialogue { 

	//On déclare nos variables
	public string name; 

	[TextArea(3, 10)]
	public string[] sentences; 

}
