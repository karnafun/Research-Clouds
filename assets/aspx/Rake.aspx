<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rake.aspx.cs" Inherits="assets_aspx_Rake" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />

        <h3>How does it work:</h3>
        <p>
            <strong>Article:</strong> You choose one of Amit Rechavi's articles. <a target="_blank" href="https://scholar.google.co.il/citations?user=vFoitJkAAAAJ&hl=en">Link to Google Scholar</a>
            <br />
            <strong>Minimum char Length:</strong> Just for testing, better you can leave 1.
                <br />
            <strong>Maximum Words:</strong> Maximum length of a keyword phrase\sentence
                <br />
            <strong>Minimum Keyword Frequency:</strong> minumum amount of times the word has to appear in the text
                <br />
            <strong>Minimum Keyword Rating:</strong> Ratio of times word apper in text vs times appears in pharses.
        </p>

        <br />

        <br />
        <br />
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell>
                        Article:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="articleDDL"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                        Minimum Char Length:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="minCharLengthDDL">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                        Maximum Words Length:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="maxWordsLengthDDL">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell>
                        Minimum Keyword Frequency:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="minKeywordFreqDDL">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>



            <asp:TableRow>
                <asp:TableCell>
                        Minimum Keyword Rating:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList runat="server" ID="minKeywordRatingDDL">
                        <asp:ListItem>Optional</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>



            <asp:TableRow>
                <asp:TableCell>
                        Number of results:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="topTB"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Button runat="server" Text="Submit" ID="SubmitBtn" OnClick="SubmitBtn_Click" />
        <br />
        <asp:Label runat="server" ID="lbl_myBest"></asp:Label>
        <br />

        <br />
        <label>Results:</label>
        <br />
        <asp:Label runat="server" ID="lbl_res"></asp:Label>

  
    </div>
    </form>
</body>
</html>
