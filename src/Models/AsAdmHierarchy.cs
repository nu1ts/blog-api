﻿using System;
using System.Collections.Generic;

namespace blog_api.Models;

/// <summary>
/// Сведения по иерархии в административном делении
/// </summary>
public partial class AsAdmHierarchy
{
    /// <summary>
    /// Уникальный идентификатор записи. Ключевое поле
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Глобальный уникальный идентификатор объекта
    /// </summary>
    public long? Objectid { get; set; }

    /// <summary>
    /// Идентификатор родительского объекта
    /// </summary>
    public long? Parentobjid { get; set; }

    /// <summary>
    /// ID изменившей транзакции
    /// </summary>
    public long? Changeid { get; set; }

    /// <summary>
    /// Код региона
    /// </summary>
    public string? Regioncode { get; set; }

    /// <summary>
    /// Код района
    /// </summary>
    public string? Areacode { get; set; }

    /// <summary>
    /// Код города
    /// </summary>
    public string? Citycode { get; set; }

    /// <summary>
    /// Код населенного пункта
    /// </summary>
    public string? Placecode { get; set; }

    /// <summary>
    /// Код ЭПС
    /// </summary>
    public string? Plancode { get; set; }

    /// <summary>
    /// Код улицы
    /// </summary>
    public string? Streetcode { get; set; }

    /// <summary>
    /// Идентификатор записи связывания с предыдущей исторической записью
    /// </summary>
    public long? Previd { get; set; }

    /// <summary>
    /// Идентификатор записи связывания с последующей исторической записью
    /// </summary>
    public long? Nextid { get; set; }

    /// <summary>
    /// Дата внесения (обновления) записи
    /// </summary>
    public DateOnly? Updatedate { get; set; }

    /// <summary>
    /// Начало действия записи
    /// </summary>
    public DateOnly? Startdate { get; set; }

    /// <summary>
    /// Окончание действия записи
    /// </summary>
    public DateOnly? Enddate { get; set; }

    /// <summary>
    /// Признак действующего адресного объекта
    /// </summary>
    public int? Isactive { get; set; }

    /// <summary>
    /// Материализованный путь к объекту (полная иерархия)
    /// </summary>
    public string? Path { get; set; }
}
