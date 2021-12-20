using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HSVPicker;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class FontParameters : MonoBehaviour
{
    public InputField input;
    public Toggle allCaps;
    public TextMeshPro text_bg, text_highlight, text_highlight2, text_main, text_secondary, text_under;

    public ColorPicker main_color, secondary_color, under_color, highlight_color, border_color, outline_color, shadow_color;
    public Dropdown colorTypeDropdown;

    public Slider outline_slider, border_slider, shade_slider, shadow_slider, blur_slider, x_slider, y_slider, hue_slider, sat_slider, highlight_slider;
    public Text outlineVal, borderVal, shadeVal, shadowVal, blurVal, xVal, yVal, hueVal, satVal, highVal;

    public VolumeProfile volume;
    private ColorAdjustments colorAdj;

    private Color initMain, initSec, initUnder, initHigh, initBor, initOut, initShad;
    private float initOutVal, initBorVal, initShadeVal, initShadVal, initBlurVal, initXVal, initYVal, initHighVal;
    private int initHueVal, initSatVal;

    private int highlightAlign;

    public TransparentBackgroundScreenshotRecorder BGCapture;

    public GameObject UI;

    private string text;

    void Start()
    {
        input.text = "GEOMETRY DASH";

        initMain = main_color.CurrentColor;
        initSec = secondary_color.CurrentColor;
        initUnder = under_color.CurrentColor;
        initHigh = highlight_color.CurrentColor;
        initBor = border_color.CurrentColor;
        initOut = outline_color.CurrentColor;
        initShad = shadow_color.CurrentColor;
        initOutVal = outline_slider.value;
        initBorVal = border_slider.value;
        initShadeVal = shade_slider.value;
        initShadVal = shadow_slider.value;
        initBlurVal = blur_slider.value;
        initXVal = x_slider.value;
        initYVal = y_slider.value;
        initHueVal = (int)hue_slider.value;
        initSatVal = (int)sat_slider.value;
        initHighVal = highlight_slider.value;

        if (!volume.TryGet(out colorAdj)) throw new System.NullReferenceException(nameof(colorAdj));
    }

    void LateUpdate()
    {
        text = allCaps.isOn ? input.text.ToUpper() : input.text;
        text_bg.text = text;
        text_highlight.text = text;
        text_highlight2.text = text;
        text_main.text = text;
        text_secondary.text = text;
        text_under.text = text;

        text_main.color = main_color.CurrentColor;
        text_secondary.color = secondary_color.CurrentColor;
        text_main.GetComponent<TMP_Text>().outlineColor = border_color.CurrentColor;
        text_secondary.GetComponent<TMP_Text>().outlineColor = border_color.CurrentColor;
        text_under.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, under_color.CurrentColor);
        text_highlight.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, highlight_color.CurrentColor);
        text_highlight2.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, highlight_color.CurrentColor);
        text_bg.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, shadow_color.CurrentColor);
        text_bg.GetComponent<TMP_Text>().outlineColor = outline_color.CurrentColor;

        text_main.GetComponent<TMP_Text>().outlineWidth = border_slider.value/100f;
        text_secondary.GetComponent<TMP_Text>().outlineWidth = border_slider.value / 100f;
        text_main.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, border_slider.value / 100f);
        text_secondary.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, border_slider.value / 100f);
        text_bg.GetComponent<TMP_Text>().outlineWidth = outline_slider.value / 100f;
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, outline_slider.value / 100f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, shadow_slider.value / 100f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, x_slider.value / 10f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, y_slider.value / 10f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlaySoftness, blur_slider.value / 100f);
        text_under.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, shade_slider.value / 100f);

        text_highlight.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, -1 * (highlight_slider.value * 2 / 100));
        text_highlight.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, ((highlightAlign == 0 || highlightAlign == 2) ? 1 : -1) * (highlight_slider.value * 2 / 100));
        text_highlight.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, ((highlightAlign == 2 || highlightAlign == 3) ? 1 : -1) * (highlight_slider.value * 2 / 100));
        text_highlight2.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, -1 * (highlight_slider.value * 2 / 100));
        text_highlight2.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, ((highlightAlign == 0 || highlightAlign == 2) ? 1 : -1) * (highlight_slider.value * 2 / 100));
        text_highlight2.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, ((highlightAlign == 2 || highlightAlign == 3) ? 1 : -1) * (highlight_slider.value * 2 / 100));

        colorAdj.hueShift.Override(hue_slider.value);
        colorAdj.saturation.Override(sat_slider.value);

        outlineVal.text = "Outline: " + outline_slider.value;
        borderVal.text = "Border: " + border_slider.value;
        shadeVal.text = "Shade: " + shade_slider.value;
        shadowVal.text = "Shadow: " + shadow_slider.value;
        blurVal.text = "Blur: " + blur_slider.value;
        xVal.text = "X: " + x_slider.value;
        yVal.text = "Y: " + y_slider.value;
        hueVal.text = "Hue: " + (hue_slider.value >= 0 ? "+" : "") + hue_slider.value;
        satVal.text = "Saturation: " + (sat_slider.value >= 0 ? "+" : "") + sat_slider.value;
        highVal.text = "Highlight: " + highlight_slider.value;

        main_color.gameObject.SetActive(colorTypeDropdown.value == 0);
        secondary_color.gameObject.SetActive(colorTypeDropdown.value == 1);
        under_color.gameObject.SetActive(colorTypeDropdown.value == 2);
        highlight_color.gameObject.SetActive(colorTypeDropdown.value == 3);
        border_color.gameObject.SetActive(colorTypeDropdown.value == 4);
        outline_color.gameObject.SetActive(colorTypeDropdown.value == 5);
        shadow_color.gameObject.SetActive(colorTypeDropdown.value == 6);

        if(Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.F11))
        {
            UI.SetActive(!UI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.F12))
        {
            BGCapture.Capture();
        }
    }

    public void setHighlightAlign(int i)
    {
        highlightAlign = i;
        text_highlight.gameObject.SetActive(highlightAlign == 0 || highlightAlign == 1);
        text_highlight2.gameObject.SetActive(highlightAlign == 2 || highlightAlign == 3);
    }

    public void Reset()
    {
        main_color.CurrentColor = initMain;
        secondary_color.CurrentColor = initSec;
        under_color.CurrentColor = initUnder;
        highlight_color.CurrentColor = initHigh;
        border_color.CurrentColor = initBor;
        outline_color.CurrentColor = initOut;
        shadow_color.CurrentColor = initShad;
        shadow_slider.value = initShadVal;
        shade_slider.value = initShadeVal;
        blur_slider.value = initBlurVal;
        x_slider.value = initXVal;
        y_slider.value = initYVal;
        outline_slider.value = initOutVal;
        border_slider.value = initBorVal;
        hue_slider.value = initHueVal;
        sat_slider.value = initSatVal;
        highlight_slider.value = initHighVal;
        highlightAlign = 0;
        text_highlight.gameObject.SetActive(true);
        text_highlight2.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        Reset();

        text_main.color = main_color.CurrentColor;
        text_secondary.color = secondary_color.CurrentColor;
        text_main.GetComponent<TMP_Text>().outlineColor = border_color.CurrentColor;
        text_secondary.GetComponent<TMP_Text>().outlineColor = border_color.CurrentColor;
        text_under.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, under_color.CurrentColor);
        text_highlight.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, highlight_color.CurrentColor);
        text_highlight2.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, highlight_color.CurrentColor);
        text_bg.fontSharedMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, shadow_color.CurrentColor);
        text_bg.GetComponent<TMP_Text>().outlineColor = outline_color.CurrentColor;

        text_main.GetComponent<TMP_Text>().outlineWidth = border_slider.value / 100f;
        text_secondary.GetComponent<TMP_Text>().outlineWidth = border_slider.value / 100f;
        text_main.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, border_slider.value / 100f);
        text_secondary.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, border_slider.value / 100f);
        text_bg.GetComponent<TMP_Text>().outlineWidth = outline_slider.value / 100f;
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, outline_slider.value / 100f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, shadow_slider.value / 100f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, x_slider.value / 10f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, y_slider.value / 10f);
        text_bg.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlaySoftness, blur_slider.value / 100f);
        text_under.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, shade_slider.value / 100f);

        text_highlight.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, -1 * (highlight_slider.value * 2 / 100));
        text_highlight.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, ((highlightAlign == 0 || highlightAlign == 2) ? 1 : -1) * (highlight_slider.value * 2 / 100));
        text_highlight.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, ((highlightAlign == 2 || highlightAlign == 3) ? 1 : -1) * (highlight_slider.value * 2 / 100));
        text_highlight2.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, -1 * (highlight_slider.value * 2 / 100));
        text_highlight2.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetX, ((highlightAlign == 0 || highlightAlign == 2) ? 1 : -1) * (highlight_slider.value * 2 / 100));
        text_highlight2.fontSharedMaterial.SetFloat(ShaderUtilities.ID_UnderlayOffsetY, ((highlightAlign == 2 || highlightAlign == 3) ? 1 : -1) * (highlight_slider.value * 2 / 100));

        colorAdj.hueShift.Override(hue_slider.value);
        colorAdj.saturation.Override(sat_slider.value);
    }
}
