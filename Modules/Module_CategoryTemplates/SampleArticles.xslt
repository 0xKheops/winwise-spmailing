<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="items">
    <h2>
      <xsl:value-of select="@CategoryLabel" />
    </h2>
    <table>
      <xsl:apply-templates />
    </table>
  </xsl:template>

  <xsl:template match="item">

    <tr>
      <td valign="top">
        <xsl:if test="@PublishingRollupImage != ''">
          <img width="80" height="80" src="{@PublishingRollupImage.src}" alt="{@PublishingRollupImage.alt}" style="border:1px solid #676767;margin-bottom:8px;margin-right:8px;" />
        </xsl:if>
      </td>
      <td valign="top" style="font-family:Tahoma, Arial, sans-serif;font-size:10pt;">
        <h3 style="margin:0px;padding:0px;">
          <a alt="@Title" href="{concat(@EncodedAbsUrl,substring-after(@FileRef,'#'))}" style="color:#0072bc;">
            <xsl:value-of select="@Title" />
          </a>
        </h3>
        <xsl:if test="@Comments != ''">
          <span style="color:#4c4c4c;">
            <xsl:value-of select="@Comments" />
          </span>
        </xsl:if>
      </td>
    </tr>

  </xsl:template>


</xsl:stylesheet>
