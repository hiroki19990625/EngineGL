using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EngineGL.Editor.Core.Parser
{
    public class CSharpCodeParser
    {
        private CompilationUnitSyntax _unit;

        public CSharpCodeParser(CompilationUnitSyntax compilationUnit)
        {
            _unit = compilationUnit;
        }

        public CodeCompileUnit Parse()
        {
            CodeCompileUnit unit = new CodeCompileUnit();

            CodeNamespace nameSpace = new CodeNamespace();
            nameSpace.Comments.Clear();

            nameSpace.Imports.AddRange(GetImports(_unit.Usings));

            CodeNamespace bodyNameSpace = GetNamespace();

            unit.Namespaces.Add(nameSpace);
            unit.Namespaces.Add(bodyNameSpace);

            return unit;
        }

        public CodeNamespaceImport[] GetImports(SyntaxList<UsingDirectiveSyntax> usingDirectiveList)
        {
            CodeNamespaceImport[] imports = new CodeNamespaceImport[usingDirectiveList.Count];
            for (int i = 0; i < imports.Length; i++)
            {
                imports[i] = new CodeNamespaceImport(usingDirectiveList[i].Name.ToString());
            }

            return imports;
        }

        public CodeNamespace GetNamespace()
        {
            CodeNamespace codeNamespace = new CodeNamespace("DefaultNamespace");
            foreach (MemberDeclarationSyntax member in _unit.Members)
            {
                if (member is NamespaceDeclarationSyntax namespaceDeclaration)
                {
                    codeNamespace.Name = namespaceDeclaration.Name.ToString();
                    codeNamespace.Imports.AddRange(GetImports(namespaceDeclaration.Usings));
                    codeNamespace.Types.AddRange(GetTypes(namespaceDeclaration.Members));
                }
            }

            return codeNamespace;
        }

        public CodeTypeDeclaration[] GetTypes(SyntaxList<MemberDeclarationSyntax> typeDeclarationList)
        {
            List<CodeTypeDeclaration> classes = new List<CodeTypeDeclaration>();
            for (int i = 0; i < typeDeclarationList.Count; i++)
            {
                MemberDeclarationSyntax syntax = typeDeclarationList[i];
                CodeTypeDeclaration type = new CodeTypeDeclaration();
                if (syntax is ClassDeclarationSyntax classDeclaration)
                {
                    type.Name = classDeclaration.Identifier.Text;
                    classes.Add(type);
                }
                else if (syntax is StructDeclarationSyntax structDeclaration)
                {
                    type.Name = structDeclaration.Identifier.Text;
                    type.IsStruct = true;
                    classes.Add(type);
                }
                else if (syntax is InterfaceDeclarationSyntax interfaceDeclaration)
                {
                    type.Name = interfaceDeclaration.Identifier.Text;
                    type.IsInterface = true;
                    classes.Add(type);
                }
                else if (syntax is EnumDeclarationSyntax enumDeclaration)
                {
                    type.Name = enumDeclaration.Identifier.Text;
                    type.IsEnum = true;
                    classes.Add(type);
                }
                else
                {
                    MessageBox.Show(syntax.GetType().Name);
                }
            }

            return classes.ToArray();
        }
    }
}