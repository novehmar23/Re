const { By, Key, Builder } = require("selenium-webdriver");
const { findConfiguration } = require("tslint/lib/configuration");

require("chromedriver");

async function test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos() {
    let driver = await new Builder().forBrowser("chrome").build();

    //Log in exitoso (como admin)
    await driver.get("http://localhost:4200/login");
    await driver.findElement(By.name("username")).sendKeys("admin");
    await driver.findElement(By.name("password")).sendKeys("admin");
    await driver.findElement(By.name("logInButton")).click(); //Clasificación exitosa
    //await driver.findElement(By.id("classifyButton-1")).click();
    await driver.get("http://localhost:4200/admin/bug/classify?id=1");
    await driver.findElement(By.name("name")).sendKeys("fake name");
    await driver.findElement(By.name("description")).sendKeys("fake description");
    await driver.findElement(By.name("comments")).sendKeys("fake comment");
    await driver.findElement(By.name("value")).sendKeys("Alta");
    await driver.findElement(By.name("save")).click();
    if (await driver.findElement(By.id("Bug Type added successfully"))) {
        console.log("success");
    } else {
        console.log("fail");
    }
    await driver.quit();
}
test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos();