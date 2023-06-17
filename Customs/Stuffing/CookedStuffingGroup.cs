using Kitchen;
using KitchenData;
using KitchenEasyStuff;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyStuff.Customs
{
    internal class CookedStuffingGroup : CustomItem
    {
        public override string UniqueNameID => "CookedStuffingGroup";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Stuffing");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemValue ItemValue => ItemValue.Medium;
        public override string ColourBlindTag => "Stuff";
        public override bool SplitByCopying => true;
        public override float SplitSpeed => 2.5f;
        public override Item SplitSubItem => Mod.StuffingPortion;
        public override List<Item> SplitDepletedItems => new List<Item> { Mod.StuffingPortion };
        public override int SplitCount => 2;

        public override void OnRegister(Item gameDataObject)
        {
            Prefab.ApplyMaterialToChild("BreadcrumbsBowl/Cylinder", "Metal Dark");
            for(int i = 1; i < 4; i++)
            {
                for(int j = 1; j < 3; j++)
                {
                    Prefab.ApplyMaterialToChild($"Portion{i}/Ice Cream{j}", "Bread - Inside Cooked");
                }
            }


            var view = Prefab.AddComponent<CookedStuffingGroupView>();

            GameObject[] list = new GameObject[]
            {
                Prefab.GetChild("Portion2"),
                Prefab.GetChild("Portion3"),
            };
            view.m_Objects = list;
        }
    }

    public class CookedStuffingGroupView : ObjectsSplittableView
    {
        public GameObject[] m_Objects;
        private ViewData m_Data;
        private bool m_RunOnce = false;
        protected override void UpdateData(ViewData data)
        {
            if (m_RunOnce && data.Remaining == 0)
            {
                return;
            }
            else
            {

                for (int i = 0; i < m_Objects.Length; i++)
                {

                    GameObject gameObject = m_Objects[i];
                    gameObject.SetActive(i < data.Remaining);
                }

                m_Data = data;
                m_RunOnce = true;
            }
        }

    }
}
