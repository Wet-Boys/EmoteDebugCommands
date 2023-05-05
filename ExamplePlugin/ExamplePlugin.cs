using BepInEx;
using BepInEx.Configuration;
using EmotesAPI;
using R2API;
using R2API.Utils;
using RiskOfOptions;
using RiskOfOptions.Options;
using RoR2;
using UnityEngine;

namespace ExamplePlugin
{
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class ExamplePlugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.weliveinasociety.EmoteDebugCommands";
        public const string PluginAuthor = "Nunchuk";
        public const string PluginName = "Emote Debug Commands";
        public const string PluginVersion = "1.0.0";

        public static ConfigEntry<KeyboardShortcut> TPoseButton;
        public void Awake()
        {
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyHonda");
            CustomEmotesAPI.BlackListEmote("EnemyHonda");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyDodge");
            CustomEmotesAPI.BlackListEmote("EnemyDodge");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyStand");
            CustomEmotesAPI.BlackListEmote("EnemyStand");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyJoin");
            CustomEmotesAPI.BlackListEmote("EnemyJoin");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyRPS");
            CustomEmotesAPI.BlackListEmote("EnemyRPS");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyConga");
            CustomEmotesAPI.BlackListEmote("EnemyConga");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyNone");
            CustomEmotesAPI.BlackListEmote("EnemyNone");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyGetDown");
            CustomEmotesAPI.BlackListEmote("EnemyGetDown");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyBreak");
            CustomEmotesAPI.BlackListEmote("EnemyBreak");
            CustomEmotesAPI.AddNonAnimatingEmote("SpawnBody");
            CustomEmotesAPI.BlackListEmote("SpawnBody");
            CustomEmotesAPI.AddNonAnimatingEmote("EnemyKazotsky");
            CustomEmotesAPI.BlackListEmote("EnemyKazotsky");
            CustomEmotesAPI.animChanged += CustomEmotesAPI_animChanged;
        }

        void DEBUGHANDLE(BoneMapper mapper, string newAnimation)
        {
            if (mapper.worldProp)
            {
                return;
            }
            if (newAnimation == "EnemyHonda")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("HondaStep", item);
                    }
                }
            }
            if (newAnimation == "EnemyKazotsky")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("Kazotsky Kick", item);
                    }
                }
            }
            if (newAnimation == "EnemyNone")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("none", item);
                    }
                }
            }
            if (newAnimation == "EnemyConga")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("Conga", item);
                    }
                }
            }
            if (newAnimation == "EnemyRPS")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("Rock Paper Scissors", item);
                    }
                }
            }
            if (newAnimation == "EnemyJoin")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    BoneMapper nearestMapper = null;
                    if (item.transform.parent.GetComponent<CharacterModel>().body.masterObject?.GetComponent<PlayerCharacterMasterController>() == null)
                    {
                        if (item.currentEmoteSpot)
                        {
                            item.JoinEmoteSpot();
                        }
                        else
                        {
                            foreach (var otherMapper in CustomEmotesAPI.GetAllBoneMappers())
                            {
                                try
                                {
                                    if (otherMapper != item)
                                    {
                                        if (!nearestMapper && (otherMapper.currentClip.syncronizeAnimation || otherMapper.currentClip.syncronizeAudio))
                                        {
                                            nearestMapper = otherMapper;
                                        }
                                        else if (nearestMapper)
                                        {
                                            if ((otherMapper.currentClip.syncronizeAnimation || otherMapper.currentClip.syncronizeAudio) && Vector3.Distance(item.transform.position, otherMapper.transform.position) < Vector3.Distance(item.transform.position, nearestMapper.transform.position))
                                            {
                                                nearestMapper = otherMapper;
                                            }
                                        }
                                    }
                                }
                                catch (System.Exception)
                                {
                                }
                            }
                            if (nearestMapper)
                            {
                                item.PlayAnim(nearestMapper.currentClip.clip[0].name, 0);
                            }
                        }
                    }
                }
            }
            if (newAnimation == "EnemyStand")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("StoodHere", item);
                    }
                }
            }
            if (newAnimation == "EnemyBreak")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("Breakneck", item);
                    }
                }
            }
            if (newAnimation == "EnemyGetDown")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("GetDown", item);
                    }
                }
            }
            if (newAnimation == "EnemyDodge")
            {
                foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
                {
                    if (item != CustomEmotesAPI.localMapper)
                    {
                        CustomEmotesAPI.PlayAnimation("DuckThisOneIdle", item);
                    }
                }
            }
            if (newAnimation == "SpawnBody")
            {
                switch (Random.Range(0, 12))
                {
                    case 0:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Acrid");
                        break;
                    case 1:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Artificer");
                        break;
                    case 2:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Bandit2");
                        break;
                    case 3:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Captain");
                        break;
                    case 4:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Commando");
                        break;
                    case 5:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Engi");
                        break;
                    case 6:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Huntress");
                        break;
                    case 7:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Loader");
                        break;
                    case 8:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body MULT");
                        break;
                    case 9:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Mercenary");
                        break;
                    case 10:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body REX");
                        break;
                    case 11:
                        RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], $"spawn_body Railgunner");
                        break;

                    default:
                        break;
                }
            }
        }

        private void CustomEmotesAPI_animChanged(string newAnimation, BoneMapper mapper)
        {
            DEBUGHANDLE(mapper, newAnimation);
        }
    }
}
