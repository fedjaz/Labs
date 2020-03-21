#include <stdio.h>

void advice(){
    int math, phys, lang, mean;
    printf("%s\n", "Введите баллы по математике, физике, языку и средний балл аттестата:");
    scanf("%d %d %d %d", &math, &phys, &lang, &mean);
    int sum = math + phys + lang + mean;
    if(math > 100 || math < 0 || phys > 100 || phys < 0 || lang > 100 || lang < 0 || mean > 100 || mean < 0){
        printf("%s\n", "Попробуйте еще раз.");
    }
    else if(lang - math > 20 && lang - phys > 20){
        printf("%s\n", "Лучше поступайте в гуманитарный вуз.");
    }
    else if(sum < 250){
        printf("%s\n", "Баллы Слишком низкие, не тратьте время.");
    }
    else if(sum >= 250 && sum < 300){
        printf("%s\n", "Попробуйте поступить на ИК или РЭ.");
    }
    else if(sum >= 300 && sum < 320){
        printf("%s\n", "Попробуйте поступить на КП.");
    }
    else if(sum >= 320 && sum <= 400){
        printf("%s\n", "Попробуйте поступить на КСиС или ИТиУ.");
    }
    
}

int main(){
    while(1){
        int responce;
        printf("%s\n", "1. Вывод информации о факультете КСиС.\n2. Вывод информации о факультете ИТиУ.\n3. Вывод информации о факультете РЭ.\n4. Вывод информации о факультете ИК.\n5. Вывод информации о факультете КП.\n6. Рекомендации к поступлению\n7. Выход из программы. ");
        scanf("%d", &responce);
        switch (responce)
        {
        case 1:
            printf("%s\n", "Сегодня факультет компьютерных систем и сетей является одним из ведущих факультетов в Республике Беларусь по подготовке IТ-специалистов. По результатам проведенного в 2010 году опроса компаний-резидентов ПВТ ФКСиС занимает первое место среди факультетов вузов Беларуси, выпускники которых востребованы в Парке высоких технологий.");
            break;
        case 2:
            printf("%s\n", "Факультет является ровесником университета. Сегодня - это крупный учебный и научный центр, в котором обучается более 1700 студентов, есть свои научные школы, функционирует магистратура, аспирантура и докторантура. Обучение ведется по четырем перспективным специальностям: «Автоматизированные системы обработки информации», «Искусственный интеллект», «Информационные технологии и управление в технических системах», «Промышленная электроника».На ФИТУ готовят специалистов широкого профиля по IТ-технологиям, которые без труда займут достойное место на рынке труда.");
            break;   
        case 3:
            printf("%s\n", "Современная радиоинформационная система предназначена для извлечения, формирования, приема, передачи и обработки информации. Для решения названных задач необходимы специалисты, всесторонне подготовленные для работы с современными микро - и наноэлектронными устройствами, способные применить свои знания и талант для создания принципиально новых поколений информационных систем.");
            break;
        case 4:
            printf("%s\n", "Факультет инфокоммуникаций обеспечивает современное образование тем, кого влечет электроника и информатика, кто желает освоить инновационные электронные и информационные технологии в телекоммуникациях, кто стремиться быть востребованным в современных социальных и экономических условиях.Студенты факультета получают глубокие знания в области информационно-коммуникационных технологий, имеют навыки разработки и внедрения новых технологий в инфокоммуникациях, что делает выпускников конкурентоспособными в рыночных условиях производства и позволяет быстрее адаптироваться к повышенным научно-техническим задачам развития отрасли инфокоммуникаций.");
            break;
        case 5:
            printf("%s\n", "Специальности факультета тесно связаны между собой и ориентированы на подготовку высококвалифицированных специалистов, которые в процессе обучения осваивают самые современные средства и методы проектирования электронных систем, мобильных систем,  программно-управляемых систем и комплексов, изделий электронно-оптической и медицинской техники.  Студенты получают фундаментальные знания по основам алгоритмизации и программирования, бизнеса и права в информационных технологиях, защиты информации, операционным системам, компьютерным сетям, языкам программирования (Pascal, C, C++, C#, Java, Prolog, Ruby, Python, HTML, XML, SQL, UML и др.), программированию сетевых приложений, базам данных, системному анализу и управлению;  изучают визуальные средства разработки программных приложений, средства и технологии анализа и разработки информационных систем, распределенные информационные системы, web-дизайн и шаблоны проектирования, экономико-математические методы и модели, бизнес-анализ и эконометрику. Получая классическое университетское образование, а также серьезную подготовку в области самых современных IT-технологий, выпускники всех специальностей факультета являются одними из самых востребованных и высокооплачиваемых, а лучшие из них занимают руководящие должности как в органах государственного управления, так и в сфере IT - бизнеса, промышленности и банковской системе, часто выступают в качестве руководителей и организаторов производства, создают собственные фирмы.");
            break;
        case 6:
            advice();
            break;
        case 7:
            return 0;
        default:
            printf("%s\n", "Попробуйте еще раз.");
            break;
        }
    }


    return 0;
}