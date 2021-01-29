using UnityEngine;

public class AudioSourcePool : MonoBehaviour {

    struct PoolEntry {
        public AudioSource Source;
        public GameObject Owner;
    }

    [SerializeField] private int maxCapacity = 12;
    [SerializeField] private int initialCapacity = 3;
    private PoolEntry[] poolEntries;
    private AudioSource original;

    private void Start() {

        poolEntries = new PoolEntry[0];
        AddSources();
    }

    private void OnEnable() {
        InvokeRepeating("FreeFinishedSounds", 1.0f, 1.0f);
    }

    private void OnDisable() {
        CancelInvoke("FreeFinishedSounds");
    }

    public void GreateGenericPrefab() {
        GameObject audioSource = new GameObject("_genericAudioSource");
        original = audioSource.AddComponent<AudioSource>();
        original.transform.SetParent(transform);
    }

    public AudioSource ClaimSource(GameObject owner) {
        for(int i = 0; i < poolEntries.Length; i++) {
            if(poolEntries[i].Owner == null) {
                poolEntries[i].Owner = owner;
                return poolEntries[i].Source;
            }
        }

        if(!AddSources())
            return null;
        else
            return ClaimSource(owner);
    }

    public void FreeSourcesFor(GameObject owner) {
        for(int i = 0; i < poolEntries.Length; i++) {
            if(poolEntries[i].Owner == null) {
                if(poolEntries[i].Source.isPlaying) {
                    poolEntries[i].Source.Stop();
                }
                poolEntries[i].Owner = null;
            }
        }
    }

    private void FreeFinishedSounds() {
        int del = 0;

        for(int i = 0; i < poolEntries.Length; i++) {
            if(poolEntries[i].Owner != null && !poolEntries[i].Source.isPlaying) {
                del++;
                poolEntries[i].Owner = null;
            }
        }

        if(del >0)
            Debug.Log("AudioSourcePool >> " + del + " sources freed.");
    }

    private bool AddSources() {
        if(poolEntries.Length + initialCapacity > maxCapacity)
            return false;

        System.Array.Resize(ref poolEntries, poolEntries.Length + initialCapacity);

        for(int i = 0; i < poolEntries.Length; i++) {
            if(poolEntries[i].Source == null) {
                poolEntries[i].Source = Instantiate(original, Vector3.zero, Quaternion.identity, transform);
                poolEntries[i].Source.transform.SetParent(transform, false);
            }
        }

        return true;
    }
}
