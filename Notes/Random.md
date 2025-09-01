Mono
https://www.mono-project.com/download/stable/#download-mac

https://www.nuget.org/packages/csharp-ls

.Net
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-9.0.8-macos-arm64-installer?cid=getdotnetcore

Remove mono
sudo rm -rf /Library/Frameworks/Mono.framework
sudo pkgutil --forget com.xamarin.mono-MDK.pkg
sudo rm /etc/paths.d/mono-commands

If you want to use a custom theme, create a file with the theme's name (e.g., `mytheme.toml`) and place it in the `~/.config/helix/themes` directory. After placing the file, you can load it using the `:theme` command or by setting it in your `config.toml` file.

```
    public class Hitbox : MonoBehaviour {
        public float tickRate = 1f;
        private bool canDamage = true;
        
        private void OnTriggerStay2D(Collider2D other) {
            Debug.Log(canDamage);
            if (other.CompareTag("Enemy") && canDamage) {
                // Player.instance.DamagePlayer(Enemies.Enemy.instance.damageToPlayer);
                StartCoroutine(TriggerDamage(other));
            }
        }

        private IEnumerator TriggerDamage(Collider2D other) {
            canDamage = false;
            Player.instance.DamagePlayer(Enemies.Enemy.instance.damageToPlayer);
            yield return new WaitForSeconds(tickRate);
            canDamage = true;
            Debug.Log(canDamage);
        }
    }
```

Other option I could use if needed
```using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float tickRate = 1f;

    private readonly Dictionary<Collider2D, Coroutine> ticking = new();

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy")) return;
        if (ticking.ContainsKey(other)) return;

        var co = StartCoroutine(DamageTick(other));
        ticking.Add(other, co);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (ticking.TryGetValue(other, out var co)) {
            StopCoroutine(co);
            ticking.Remove(other);
        }
    }

    private IEnumerator DamageTick(Collider2D enemy) {
        while (true) {
            // Adjust to your damage call; avoid null refs if enemy dies
            var enemyComp = enemy ? enemy.GetComponent<Enemies.Enemy>() : null;
            var dmg = enemyComp ? enemyComp.damageToPlayer : 1;

            Player.instance.DamagePlayer(Enemies.Enemy.instance.damageToPlayer);
            yield return new WaitForSeconds(tickRate);
        }
    }
}

```

