<%@ Page Title="ExcelUpload" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExcelUpload.aspx.cs" Inherits="Crud_Practice1.ExcelUpload" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1 id="aspnetTitle">CRUD</h1>

        <div class="card-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form-group">
                        <p class="lead">INSERT EXCEL FILE HERE</p>
                    </div>
                    <div class="container">
                        <h2 class="mt-4">Mass Upload Students</h2>
                        <div class="form-group">
                            <asp:FileUpload ID="fuUpload" runat="server" AllowMultiple="false" accept=".xlsx,.xls" CssClass="form-control" ClientIDMode="Static" />
                        </div>
                        <asp:Button ID="btnUpload" Text="Upload" runat="server" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
                        <br />
                        <asp:Label ID="lblMessage" runat="server" CssClass="mt-3" ForeColor="Green"></asp:Label>
                    </div>
                </div>
                <div class="form-row mt-2">

                    <div class="col-lg-12 mt-2">
                        <br />
                        <b>Upload Result</b>
                        <br />
                        <label class="red-text" id="lblErrorFound" runat="server">Errors Found. Please fix the errors below then re-upload the corrected file.</label>
                        <label class="green-text" id="lblSuccess" runat="server">Successfully Uploaded. Please see below created MR's</label>
                        <div class="form-row">
                            <div class="col-lg-3 mt-2">
<%--                                <asp:LinkButton ID="lnkDownloadValidateFile" runat="server" CssClass="btn btn-block btn-success btn-xs" OnClick="lnkDownloadValidateFile_Click"><i class="fa fa-file-signature"></i>&nbsp;Download Validated File</asp:LinkButton>--%>
                            </div>
                        </div>
                        <div class="table-responsive mt-2">
                            <asp:GridView ID="gvUploadResult" runat="server" CssClass="mydatagrid small" AutoGenerateColumns="false" ShowHeader="true" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header text-nowrap" FooterStyle-CssClass="header text-nowrap font-weight-bold" RowStyle-CssClass="rows">
                                <EmptyDataTemplate>
                                    <b>&nbsp;</b>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ToolTip="Error" ID="lblError" runat="server" Visible='<%# ((Eval("type").ToString()=="ERROR" ? true:false)) %>'><i class="fa fa-exclamation-triangle red-text"></i></asp:Label>
                                            <asp:Label ToolTip="Success" ID="lblSuccess" runat="server" Visible='<%# ((Eval("type").ToString()=="SUCCESS" ? true:false)) %>'><i class="fa fa-check-circle green-text"></i></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="row_no" HeaderText="Row No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="5%" />
                                    <asp:BoundField DataField="row_value" HeaderText="Row Value" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="40%" />
                                    <asp:BoundField DataField="resolution" HeaderText="Resolution" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="40%" />

                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvUploadResultSuccess" runat="server" CssClass="mydatagrid small" AutoGenerateColumns="false" ShowHeader="true" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header text-nowrap" FooterStyle-CssClass="header text-nowrap font-weight-bold" RowStyle-CssClass="rows">
                                <EmptyDataTemplate>
                                    <b>&nbsp;</b>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ToolTip="Success" ID="lblSuccess" runat="server"><i class="fa fa-check-circle green-text"></i></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MR Control No." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkMRNo" runat="server" OnClientClick='<%# ("openMR(" + Eval("mr_id").ToString() + ")") %>'><u><%# Eval("mr_control_no") %></u></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="customer_code" HeaderText="Customer Code" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="customer_name" HeaderText="Customer Name" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="warehouse_name" HeaderText="Warehouse" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="principal_name" HeaderText="Principal" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="delivery_option" HeaderText="Delivery Option" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="deliver_to" HeaderText="Deliver To" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="delivery_address" HeaderText="Delivery Address" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Wrap="false" ItemStyle-Width="20%" />

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
