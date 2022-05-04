Imports Newtonsoft.Json

Partial Public Class DesignGearModelPackage
    Public Const DesignGearPackageDataFileName = "DesignGearPackageContents.json"

    Public Function SavePackage(dir As String) As String
        If Not IO.Directory.Exists(dir) Then
            IO.Directory.CreateDirectory(dir)
        End If

        Dim filePath = IO.Path.Combine(dir, DesignGearPackageDataFileName)

        'Save 
        IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(Me, Formatting.Indented))

        Return filePath
    End Function

    Public Enum ValueTypeEnum As Integer
        [String]

        [Double]

        [Boolean]
    End Enum

    Public Enum DescriptionClassificationEnum As Integer

        Text

        Image

        Drawing

        Attachment

        Reference
    End Enum
End Class

