<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Attachments.aspx.cs" Inherits="Crud_Practice1.Attachments" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <h3>Attachments</h3>
        <asp:GridView ID="AttachmentsGridView" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="AttachmentID" HeaderText="Attachment ID" />
                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandArgument='<%# Eval("AttachmentID") %>' OnClick="lnkDownload_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>

</asp:Content>