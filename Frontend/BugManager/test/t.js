const { By, Builder } = require("selenium-webdriver");
let res = true;
require("chromedriver");

const test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos =
    async() => {
        try {
            let driver = await new Builder().forBrowser("chrome").build();

            //Log in exitoso (como admin)
            await driver.get("http://localhost:4200/login");
            await driver.findElement(By.name("username")).sendKeys("admin");
            await driver.findElement(By.name("password")).sendKeys("admin");
            setTimeout(async() => {
                await driver.findElement(By.name("logInButton")).click();
                //Clasificación exitosa
                setTimeout(async() => {
                    //await driver.findElement(By.id("classifyButton-1")).click();
                    await driver.get("http://localhost:4200/admin/bug/classify?id=1");
                    setTimeout(async() => {
                        await driver.findElement(By.name("name")).sendKeys("fake name");
                        await driver
                            .findElement(By.name("description"))
                            .sendKeys("fake description");
                        await driver
                            .findElement(By.name("comments"))
                            .sendKeys("fake comment");
                        await driver.findElement(By.name("value")).sendKeys("Alta");
                        setTimeout(async() => {
                            await driver.findElement(By.name("save")).click();
                            setTimeout(async() => {
                                let testSuccess = false;
                                if (
                                    await driver.findElement(By.id("Bug Type added successfully"))
                                ) {
                                    response = true;
                                }
                                console.log("dentro del test: " + response);
                                await driver.quit();
                                res = res && response;
                                return res;
                            }, 1000);
                        }, 1000);
                    }, 1000);
                }, 1000);
            }, 1000);
        } catch (error) {
            console.log("error");
        }
    };

async function allTests() {
    test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos();
}

allTests();