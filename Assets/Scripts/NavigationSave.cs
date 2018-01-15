using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationSave : MonoBehaviour {

    [SerializeField]
    private TrailRenderer trail;
    public Transform player;
    [Header("Start nav recording")]
    public bool saveNav = false;
    [Header("Nav save json with position recording")]
    public bool saveJsonNav = false;   
    [Header("Draw the player path")]
    public bool drawRay = false;
    [Header("Nav save to Json with trail")]
    public bool exportTrailJson = false;
    [Header("Import from Json")]
    public bool importJson = false;
    private List<Vector3> listPos = new List<Vector3>();



    IEnumerator Timer()
    {
        print(Time.time);
        SaveNavigation();
        yield return new WaitForSeconds(1f);
        print(Time.time);
    }

    // Use this for initialization
    void Start () {
	 	
	}
	
	// Update is called once per frame
	void Update () {
        if (saveNav)
        {
            StartCoroutine("Timer");
            
        }
        if (saveJsonNav) {
            saveNav = false;
            WritePositions();
        }
        saveJsonNav = false;
        if (exportTrailJson)
        {
            trail.enabled = true;
            ExportTrailJson();
        }
        exportTrailJson = false;

        if (importJson)
        {
            ImportPositions();
        }
        importJson = false;
        if (drawRay)
        {
            DrawMovement();
            trail.enabled = false;
        }
        if (!drawRay)
        {
            trail.enabled = true;
        }
        
	}
    void SaveNavigation() {
        if (listPos.Count == 0  )
        {
            listPos.Add(player.position);
        }
        else if (listPos[listPos.Count-1] != player.position) {
            listPos.Add(player.position);
        }
    }

    List<Vector3> SaveListTrail()
    {
        listPos.Clear();
        for (int i = 0; i < trail.positionCount; i++) {
            
            listPos.Add(trail.GetPosition(i));
        }
        return listPos;
    }
    void ExportTrailJson() {
        WriteFileUtil.ListToJson<Vector3>("Assets/Json/test.json", SaveListTrail());
    }

    void ImportTrail() {
       listPos= WriteFileUtil.ListFromJson<Vector3>("Assets/Json/test.json");

        trail.Clear();
        for (int i = 0;i<listPos.Count ;i++) {


            
            
        }
    }
    void WritePositions() {
        WriteFileUtil.ListToJson<Vector3>("Assets/Json/test.json",listPos);
    }
    void ImportPositions()
    {
        listPos = WriteFileUtil.ListFromJson<Vector3>("Assets/Json/test.json");
        player.position = listPos[listPos.Count - 1];
    }
    void DrawMovement()
    {
        for (int i = 0; i < listPos.Count; i++)
        {

            if(i+1 != listPos.Count && listPos.Count>0)
            {
                Debug.DrawRay(listPos[i], listPos[i]- listPos[i+1],Color.red);
            }


        }
    }
}
