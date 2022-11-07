Feature: ClasificacionBugs


@tag1
Scenario: Clasificación de prioridad de un bug con datos válidos
	Given the name is usuario no unico
	And  the description is acepta más de un usuario con el mismo nombre
	And  the comments is solo esta único el id
	And  the value is Alta
	When Click Classification button
	Then Expect Code 200



Scenario: Clasificación de prioridad de un bug con nombre invalido
	Given the name is vacio
	And  the description is acepta más de un usuario con el mismo nombre
	And  the comments is solo esta único el id
	And  the value is Alta
	When Click Classification button
	Then Expect Code 400


	
Scenario: Clasificación de prioridad de un bug con descripción invalida
	Given the name is usuario repetido
	And  the description is vacia
	And  the comments is solo esta único el id
	And  the value is Alta
	When Click Classification button
	Then Expect Code 400


Scenario: Clasificación de prioridad de un bug con comentario invalido
	Given the name is usuario repetido
	And  the description is acepta más de un usuario con el mismo nombre
	And  the comments is vacio
	And  the value is Alta
	When Click Classification button
	Then Expect Code 400


Scenario: Clasificación de prioridad de un bug con valor invalido
	Given the name is usuario no unico
	And  the description is acepta más de un usuario con el mismo nombre
	And  the comments is solo esta único el id
	And  the value is Holaaa
	When Click Classification button
	Then Expect Code 400


