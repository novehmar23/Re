const { By, Key, Builder } = require("selenium-webdriver");

require("chromedriver");

async function test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos() {
    let driver = await new Builder().forBrowser("chrome").build();
    const winHandleBefore = driver.getWindowHandle();

    //Log in exitoso (como admin)
    await driver.get("http://localhost:4200/login");
    await driver.findElement(By.name("username")).sendKeys("admin");
    await driver.findElement(By.name("password")).sendKeys("admin");
    await driver.findElement(By.name("logInButton")).click();

    setInterval(async function() {
        //Clasificación exitosa
        await driver.get("http://localhost:4200/admin/bug/classify?id=1");
        //await driver.findElement(By.name("classifyButton-1")).click();
        await driver.findElement(By.name("name")).sendKeys("fake name");
        await driver
            .findElement(By.name("description"))
            .sendKeys("fake description");
        await driver.findElement(By.name("comments")).sendKeys("fake comment");
        await driver.findElement(By.name("value")).sendKeys("Alta");
        setInterval(async function() {
            await driver.findElement(By.name("save")).click();
            if (
                await driver
                .findElement(By.name("Bug Type added successfully"))
                .isDisplayed()
            ) {
                console.log("success");
            } else {
                console.log("fail");
            }
            await driver.switchTo().window(winHandleBefore);
            await driver.quit();
        }, 1000);
    }, 2000);
}

test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos();