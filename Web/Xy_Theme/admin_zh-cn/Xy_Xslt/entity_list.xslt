<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:xyxsl="http://www.xiaoyang.com/HodeNamespace"
    exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes" omit-xml-declaration="yes"/>
  <xsl:namespace-alias stylesheet-prefix="xyxsl" result-prefix="xsl"/>

  <xsl:template match="/">
    <table class="table table-bordered table-mid">
      <thead>
        <tr>
          <xsl:for-each select="DataTable/DataItem">
            <xsl:if test="IsShow = 'True'">
              <th>
                <xsl:value-of select="Name"/>
              </th>
            </xsl:if>
          </xsl:for-each>
          <th>操作</th>
        </tr>
      </thead>
      <tbody>
        [% @Data Provider="Data" Name="EntityList" Root="<xsl:value-of select="$TypeName" />Collection/<xsl:value-of select="$TypeName" />" EnableScript="True" %]
        <tr>
          <xsl:for-each select="DataTable/DataItem">
            <xsl:if test="IsShow = 'True'">
              <td>
                <xyxsl:choose>
                  <xyxsl:when test="count({Key}) = 1">
                    <xyxsl:value-of select="{Key}" />
                  </xyxsl:when>
                  <xyxsl:otherwise>
                    <xyxsl:for-each select="{Key}">
                      <p>
                        <xyxsl:value-of select="text()" />
                      </p>
                    </xyxsl:for-each>
                  </xyxsl:otherwise>
                </xyxsl:choose>
              </td>
            </xsl:if>
          </xsl:for-each>
          <td>
              <a target="_blank" href="entity_edit_{{ID}}.{$Ext}" class="btn">修改</a>
              <a class="btn ajaxlink" ajax-confirm="您确定要删除该发布?" href="/postAction_post_del.{$Ext}" ajax-data="{{{{ ID:{{ID}} }}}}" ajax-success="function(){{{{UpdateContent();}}}}">删除</a>
          </td>
        </tr>
        [% @End %]
      </tbody>
    </table>
    <div class="row-fluid mt10">
      <div class="pagination pagination-right margin-none span12">
			  <ul>
        [% @Data Provider="Data" Name="EntityList" Root="/" %]
          <xyxsl:for-each select="{$TypeName}Collection/{$TypeName}" />
          <xyxsl:for-each select="{$TypeName}Collection/Pages" >
            <xyxsl:choose>
              <xyxsl:when test="@Current=1">
                <li class="disabled">
                  <a href="javascript:;">«</a>
                </li>
              </xyxsl:when>
              <xyxsl:otherwise>
                <li>
                  <a href="javascript:UpdateContent({{@Current+(-2)}});">«</a>
                </li>
              </xyxsl:otherwise>
            </xyxsl:choose>
            <xyxsl:for-each select="Page">
            <xyxsl:choose>
                <xyxsl:when test="@Index=/{$TypeName}Collection/Pages/@Current">
                  <li class="active">
                    <a href="javascript:;">
                      <xyxsl:value-of select="@Index"/>
                    </a>
                  </li>
                </xyxsl:when>
                <xyxsl:otherwise>
                  <li>
                    <a href="javascript:UpdateContent({{@Index+(-1)}});">
                      <xyxsl:value-of select="@Index"/>
                    </a>
                  </li>
                </xyxsl:otherwise>
              </xyxsl:choose>
            </xyxsl:for-each>
            <xyxsl:choose>
              <xyxsl:when test="@Current=@Max">
                <li class="disabled">
                  <a href="javascript:;">»</a>
                </li>
              </xyxsl:when>
              <xyxsl:otherwise>
                <li>
                  <a href="javascript:UpdateContent({{@Current}});">»</a>
                </li>
              </xyxsl:otherwise>
            </xyxsl:choose>
          </xyxsl:for-each>
        [% @End %]
        </ul>
      </div>
    </div>
  </xsl:template>
</xsl:stylesheet>
