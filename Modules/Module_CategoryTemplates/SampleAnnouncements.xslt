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
    <xsl:apply-templates />

  </xsl:template>

  <xsl:template match="item">
    <h3 style="margin:0px;padding:0px;color:#0072bc;">
      <xsl:value-of select="@Title" />
    </h3>
    <xsl:value-of select="@Body" disable-output-escaping="yes" />
  </xsl:template>


</xsl:stylesheet>
