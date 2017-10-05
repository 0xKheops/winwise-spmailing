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
    <xsl:variable name="linkUrl" select="substring-before(@URL,',')" />
    <xsl:variable name="linkDesc" select="substring-after(@URL,', ')" />
    <xsl:variable name="authorName" select="substring-after(@Author,';#')" />

    <h3 style="margin:0px;padding:0px;">
      <a href="{$linkUrl}" style="color:#0072bc;">
        <xsl:choose>
          <xsl:when test="$linkDesc != ''">
            <xsl:value-of select="$linkDesc" />
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="$linkUrl" />
          </xsl:otherwise>
        </xsl:choose>
      </a>
    </h3>
    <p style="margin-top:0px;">
      <xsl:if test="@Comments != ''">
        <xsl:value-of select="@Comments" />
      </xsl:if>
      <br />
      <xsl:text>created by : </xsl:text>
      <xsl:value-of select="$authorName" />
    </p>

  </xsl:template>


</xsl:stylesheet>
