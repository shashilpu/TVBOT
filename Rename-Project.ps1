function Rename-Project {
    
        $solutionPath="C:\Users\shashibhushan.kumar\source\Newfolder"
        $oldName="TVBot"
        $newName="ByteBaba"
    

    # Check if solution file exists
    if (!(Test-Path "$solutionPath\$oldName.sln")) {
        Write-Error "Solution file '$solutionPath\$oldName.sln' not found"
        return
    }
	else{
		Write-Output "Solution file '$solutionPath\$oldName.sln'  found"
	}

    # Rename solution file
    Rename-Item "$solutionPath\$oldName.sln" "$newName.sln"

    # Rename project folders and files
    Get-ChildItem -Path $solutionPath -Recurse -Filter "$oldName*" | ForEach-Object {
        $newPath = $_.FullName -replace [regex]::Escape($oldName), $newName
        Rename-Item -Path $_.FullName -NewName $newPath
    }

    # Update solution file references
    (Get-Content "$solutionPath\$newName.sln") -replace $oldName, $newName | Set-Content "$solutionPath\$newName.sln"

    # Update project file references
    Get-ChildItem -Path $solutionPath -Recurse -Filter "*.csproj" | ForEach-Object {
        (Get-Content $_.FullName) -replace $oldName, $newName | Set-Content $_.FullName
    }

    # Update namespaces in .cs files
    Get-ChildItem -Path $solutionPath -Recurse -Filter "*.cs" | ForEach-Object {
        (Get-Content $_.FullName) -replace $oldName, $newName | Set-Content $_.FullName
    }

    Write-Output "Renaming completed successfully."
}
Rename-Project
