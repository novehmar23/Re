const { By, Builder } = require("selenium-webdriver");
let res = undefined;
require("chromedriver");

const test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos =
    async() => {
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
                                testSuccess = true;
                            }
                            await driver.quit();
                            return testSuccess;
                        }, 1000);
                    }, 1000);
                }, 1000);
            }, 1000);
        }, 1000);
    };

const test_Clasificación_de_prioridad_de_un_bug_con_nombre_invalido =
    async() => {
        return true;
    };

const testsSucess = async() => {
    let test_1 = false;
    let test_2 = false;
    await test_Clasificacion_de_prioridad_de_un_bug_con_datos_validos().then(
        (res) => {
            test_1 = res;
            test_Clasificación_de_prioridad_de_un_bug_con_nombre_invalido().then(
                async(res2) => {
                    test_2 = res2;
                    setTimeout(async function() {
                        console.log("FIN" + test_1 && test_2);
                        return test_1 && test_2;
                    }, 10000);
                }
            );
        }
    );
};

testsSucess();