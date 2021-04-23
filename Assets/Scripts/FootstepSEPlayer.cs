using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FootstepSEPlayer : MonoBehaviour
{
  [System.Serializable]
  public class AudioClips
  {
    public string groundTypeTag;
    public AudioClip[] audioClips;
  }
  //足音の種類毎にタグ名とオーディオクリップを登録
  [SerializeField] List<AudioClips> listAudioClips = new List<AudioClips>();
  //Terrain Layersと足音判定用タグの対応関係を記入
  [SerializeField] string[] terrainLayerToTag;
  [SerializeField] bool randomizePitch = true;
  [SerializeField] float pitchRange = 0.1f;

  private Dictionary<string, int> tagToIndex = new Dictionary<string, int>();
  private int groundIndex = 0;
  private Terrain t;
  private TerrainData tData;

  protected AudioSource source;

  private void Awake()
  {
    source = GetComponents<AudioSource>()[0];

    for(int i=0; i<listAudioClips.Count(); ++i)
      tagToIndex.Add(listAudioClips[i].groundTypeTag, i);
  }

  private void Start()
  {
    //Scene内のTerrainからデータを取得
    t = Terrain.activeTerrain;
    tData = t.terrainData;
  }

  public void RelayedTrigger(Collider other)
  {
    //GameObjectに付けた足音判定用のタグを取得
    if(tagToIndex.ContainsKey(other.gameObject.tag))
      groundIndex = tagToIndex[other.gameObject.tag];

    if(other.gameObject.GetInstanceID() == t.gameObject.GetInstanceID())
    {
      //Terrainから現在地のAlphamapを取得
      Vector3 position = transform.position - t.transform.position;
      int offsetX = (int)(tData.alphamapWidth * position.x / tData.size.x);
      int offsetZ = (int)(tData.alphamapHeight * position.z / tData.size.z);
      float[,,] alphamaps = tData.GetAlphamaps(offsetX, offsetZ, 1, 1);
      //Alphamap中で成分が最大のTerrainLayerを探す
      float[] weights = alphamaps.Cast<float>().ToArray();
      int terrainLayer = System.Array.IndexOf(weights, weights.Max());
      groundIndex = tagToIndex[terrainLayerToTag[terrainLayer]];
    }
  }

  public void PlayFootstepSE()
  {
    //groundIndexで呼び出すオーディオクリップを変える
    AudioClip[] clips = listAudioClips[groundIndex].audioClips;
    if (randomizePitch)
      source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
      source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
  }
}