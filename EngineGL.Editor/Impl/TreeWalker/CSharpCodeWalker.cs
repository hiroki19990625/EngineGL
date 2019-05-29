using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EngineGL.Editor.Impl.TreeWalker
{
    public class CSharpCodeWalker : CSharpSyntaxWalker
    {
        public const int FILE = 0;
        public const int CLASS = 1;
        public const int INTERFACE = 2;
        public const int STRUCT = 3;
        public const int DELEGATE = 4;

        public const int PROPERTY = 5;
        public const int FIELD = 6;
        public const int METHOD = 7;

        private TreeNode _rootNode;

        private TreeNode _usingNode;
        private TreeNode _typeNode;

        public CSharpCodeWalker(TreeNode rootNode)
        {
            _rootNode = rootNode;

            _usingNode = rootNode.Nodes.Add("Using");
            _typeNode = _rootNode.Nodes.Add("Type");
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            if (node != null)
                _usingNode.Nodes.Add(node.Name.ToString());

            base.VisitUsingDirective(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node != null)
            {
                TreeNode n = _typeNode.Nodes.Add(node.Identifier.Text);
                n.Name = node.Identifier.Text;
                n.ImageIndex = CLASS;
                n.SelectedImageIndex = CLASS;
            }

            base.VisitClassDeclaration(node);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            if (node != null)
            {
                TreeNode n = _typeNode.Nodes.Add(node.Identifier.Text);
                n.Name = node.Identifier.Text;
                n.ImageIndex = INTERFACE;
                n.SelectedImageIndex = INTERFACE;
            }

            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            if (node != null)
            {
                TreeNode n = _typeNode.Nodes.Add(node.Identifier.Text);
                n.Name = node.Identifier.Text;
                n.ImageIndex = STRUCT;
                n.SelectedImageIndex = STRUCT;
            }

            base.VisitStructDeclaration(node);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            if (node != null)
            {
                TreeNode n = _typeNode.Nodes.Add($"{node.Identifier.Text}() : {node.ReturnType}");
                n.ImageIndex = DELEGATE;
                n.SelectedImageIndex = DELEGATE;
            }

            base.VisitDelegateDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (node != null)
            {
                SyntaxNode syntax = node.Parent;
                if (syntax is ClassDeclarationSyntax classDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(classDeclaration.Identifier.Text))
                    {
                        TreeNode n = _typeNode.Nodes[classDeclaration.Identifier.Text].Nodes
                            .Add($"{node.Identifier.Text} : {node.Type.ToString()}");
                        n.ImageIndex = PROPERTY;
                        n.SelectedImageIndex = PROPERTY;
                    }
                }
                else if (syntax is InterfaceDeclarationSyntax interfaceDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(interfaceDeclaration.Identifier.Text))
                    {
                        TreeNode n = _typeNode.Nodes[interfaceDeclaration.Identifier.Text].Nodes
                            .Add($"{node.Identifier.Text} : {node.Type.ToString()}");
                        n.ImageIndex = PROPERTY;
                        n.SelectedImageIndex = PROPERTY;
                    }
                }
                else if (syntax is StructDeclarationSyntax structDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(structDeclaration.Identifier.Text))
                    {
                        TreeNode n = _typeNode.Nodes[structDeclaration.Identifier.Text].Nodes
                            .Add($"{node.Identifier.Text} : {node.Type.ToString()}");
                        n.ImageIndex = PROPERTY;
                        n.SelectedImageIndex = PROPERTY;
                    }
                }
            }

            base.VisitPropertyDeclaration(node);
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if (node != null)
            {
                SyntaxNode syntax = node.Parent;
                if (syntax is ClassDeclarationSyntax classDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(classDeclaration.Identifier.Text))
                    {
                        SeparatedSyntaxList<VariableDeclaratorSyntax> variables = node.Declaration.Variables;
                        foreach (VariableDeclaratorSyntax variable in variables)
                        {
                            TreeNode n = _typeNode.Nodes[classDeclaration.Identifier.Text].Nodes
                                .Add($"{variable.Identifier.Text} : {node.Declaration.Type}");
                            n.ImageIndex = PROPERTY;
                            n.SelectedImageIndex = PROPERTY;
                        }
                    }
                }
                else if (syntax is InterfaceDeclarationSyntax interfaceDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(interfaceDeclaration.Identifier.Text))
                    {
                        SeparatedSyntaxList<VariableDeclaratorSyntax> variables = node.Declaration.Variables;
                        foreach (VariableDeclaratorSyntax variable in variables)
                        {
                            TreeNode n = _typeNode.Nodes[interfaceDeclaration.Identifier.Text].Nodes
                                .Add($"{variable.Identifier.Text} : {node.Declaration.Type}");
                            n.ImageIndex = PROPERTY;
                            n.SelectedImageIndex = PROPERTY;
                        }
                    }
                }
                else if (syntax is StructDeclarationSyntax structDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(structDeclaration.Identifier.Text))
                    {
                        SeparatedSyntaxList<VariableDeclaratorSyntax> variables = node.Declaration.Variables;
                        foreach (VariableDeclaratorSyntax variable in variables)
                        {
                            TreeNode n = _typeNode.Nodes[structDeclaration.Identifier.Text].Nodes
                                .Add($"{variable.Identifier.Text} : {node.Declaration.Type}");
                            n.ImageIndex = PROPERTY;
                            n.SelectedImageIndex = PROPERTY;
                        }
                    }
                }
            }

            base.VisitFieldDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node != null)
            {
                SyntaxNode syntax = node.Parent;
                if (syntax is ClassDeclarationSyntax classDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(classDeclaration.Identifier.Text))
                    {
                        TreeNode n = _typeNode.Nodes[classDeclaration.Identifier.Text].Nodes
                            .Add($"{node.Identifier.Text}() : {node.ReturnType.ToString()}");
                        n.ImageIndex = PROPERTY;
                        n.SelectedImageIndex = PROPERTY;
                    }
                }
                else if (syntax is InterfaceDeclarationSyntax interfaceDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(interfaceDeclaration.Identifier.Text))
                    {
                        TreeNode n = _typeNode.Nodes[interfaceDeclaration.Identifier.Text].Nodes
                            .Add($"{node.Identifier.Text}() : {node.ReturnType.ToString()}");
                        n.ImageIndex = PROPERTY;
                        n.SelectedImageIndex = PROPERTY;
                    }
                }
                else if (syntax is StructDeclarationSyntax structDeclaration)
                {
                    if (_typeNode.Nodes.ContainsKey(structDeclaration.Identifier.Text))
                    {
                        TreeNode n = _typeNode.Nodes[structDeclaration.Identifier.Text].Nodes
                            .Add($"{node.Identifier.Text}() : {node.ReturnType.ToString()}");
                        n.ImageIndex = PROPERTY;
                        n.SelectedImageIndex = PROPERTY;
                    }
                }
            }

            base.VisitMethodDeclaration(node);
        }
    }
}