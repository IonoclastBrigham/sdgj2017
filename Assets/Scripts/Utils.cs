using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    
    public static IEnumerator FadeToColor( UnityEngine.UI.Image img, Color start, Color end, float delay, float duration )
    {
        yield return new WaitForSeconds(delay);

        float timer = duration;
        while (timer > 0)
        {
            img.color = Color.Lerp(start, end, timer / duration);
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }
    }
}
