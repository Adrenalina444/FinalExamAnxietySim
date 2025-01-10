using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class PanelManager : Singleton<PanelManager>
{
    public List<PanelModel> Panels;

    private List<PanelInstanceModel> _listInstance = new List<PanelInstanceModel>();

    public void ShowPanel(string panelId)
    {
        PanelModel panelModel = Panels.FirstOrDefault(Panel => Panel.PanelId == panelId);

        if (panelModel != null)
        {
            var newInstancePanel = Instantiate(panelModel.PanelPrefab, transform);
            newInstancePanel.transform.localPosition = Vector3.zero;

            _listInstance.Add(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = newInstancePanel
            });

        }
        else
        {
            Debug.LogWarning($"trying to find panels, but no can do");
        }
    } 

    public void HideLastPanel()
    {
        if(AnyPanelShowing())
        {
            var lastPanel = _listInstance[_listInstance.Count - 1];

            _listInstance.Remove(lastPanel);

            Destroy(lastPanel.PanelInstance);
        }

    }
    public bool AnyPanelShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }
    public int GetAmountPanelsInQueue()
    {
        return _listInstance.Count;
        
    }
}

