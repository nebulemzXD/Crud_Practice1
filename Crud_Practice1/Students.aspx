<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="Crud_Practice1.Students" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>

        <h3>Students</h3>
        <div class="card-body">
            <div class="row">
                <div class="col-lg-6">
                    <p class="lead">INSERT DATA HERE</p>
                    <!-- Name Field -->
                    <div class="form-group">
                        <label for="txtName">Name</label>
                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <!-- Email Field -->
                    <div class="form-group">
                        <label for="txtEmail">Email</label>
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <!-- Gender Field -->
                    <div class="form-group">
                        <label for="txtGender">Gender</label>
                        <asp:TextBox ID="txtGender" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <!-- Birthdate Field -->
                    <div class="form-group">
                        <label for="txtBirthdate">Birthday</label>
                        <asp:TextBox ID="txtBirthdate" runat="server" CssClass="form-control datepicker" placeholder="yyyy/mm/dd"></asp:TextBox>
                    </div>

                    <script>
                        $(document).ready(function () {
                            $(".datepicker").datepicker({
                                dateFormat: "yy/mm/dd"
                            });
                        });
                    </script>

                    <div class="form-group">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Font-Bold="true" OnClick="btnSubmit_Click1" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" Font-Bold="true" OnClick="btnReset_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="card-footer">
            <asp:UpdatePanel ID="StudentInfo" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit" CssClass="table table-striped table-bordered" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="StudentID">
                        <Columns>
                            <asp:TemplateField HeaderText="Student ID">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnStudentID" runat="server" CommandArgument='<%# Eval("StudentID") %>' OnCommand="btnStudentID_Command" Text='<%# Eval("StudentID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gender">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtGender" runat="server" Text='<%# Bind("Gender") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblGender" runat="server" Text='<%# Bind("Gender") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Birthdate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBirthdate" runat="server" Text='<%# Bind("Birthdate", "{0:yyyy-MM-dd}") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBirthdate" runat="server" Text='<%# Bind("Birthdate", "{0:yyyy-MM-dd}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnAttachment" runat="server" Text="View Attachments" OnClick="btnAttachment_Click" CommandArgument='<%# Eval("StudentID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="modal fade" id="student_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">New message</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">

                                    <asp:TextBox ID="txtModalStudentID" runat="server" ReadOnly="true" CssClass="form-control" />
                                    <asp:TextBox ID="txtModalName" runat="server" ReadOnly="true" CssClass="form-control" />
                                    <asp:TextBox ID="txtModalEmail" runat="server" ReadOnly="true" CssClass="form-control" />
                                    <asp:TextBox ID="txtModalGender" runat="server" ReadOnly="true" CssClass="form-control" />
                                    <asp:TextBox ID="txtModalBirthdate" runat="server" ReadOnly="true" CssClass="form-control" />
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandArgument='<%# Eval("AttachmentID") %>' OnClick="lnkDownload_Click" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                          
                                    <br />
                                    <label for="fileUpload">Upload Picture</label>
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" AllowMultiple="true" />
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <asp:Button ID="btnupload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnupload_Click1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnupload" />
                </Triggers>
            </asp:UpdatePanel>
        </div>


    </main>

</asp:Content>
